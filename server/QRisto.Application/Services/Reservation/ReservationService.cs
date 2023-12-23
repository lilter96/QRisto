using AutoMapper;
using QRisto.Application.Models.Request.Reservation;
using QRisto.Application.Models.Response.Reservation;
using QRisto.Application.Utils;
using QRisto.Persistence.Entity.Provider;
using QRisto.Persistence.Repositories.Implementations;

namespace QRisto.Application.Services.Reservation;

public class ReservationService : IReservationService
{
    private readonly IMapper _mapper;
    private readonly UnitOfWork _unitOfWork;

    public ReservationService(UnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<List<ReservationRangeResponseModel>>> GetAvailableReservationsAsync(
        GetAvailableReservationsRequestModel model)
    {
        try
        {
            var operatingSchedule =
                await _unitOfWork.OperatingScheduleRepository.GetOperatingScheduleForDayAsync(model.Day);

            if (operatingSchedule is not { IsWorkingDay: true })
            {
                return Result<List<ReservationRangeResponseModel>>.Success(new List<ReservationRangeResponseModel>());
            }

            var reservations =
                await _unitOfWork.ReservationRepository.GetTableReservationsForDayAsync(model.TableId, model.Day);

            reservations ??= new List<ReservationEntity>();

            var availableSlots = CalculateAvailableSlots(
                model.Day,
                operatingSchedule.WorkingIntervals.ToList(),
                reservations);

            return Result<List<ReservationRangeResponseModel>>.Success(availableSlots);
        }
        catch (Exception ex)
        {
            return Result<List<ReservationRangeResponseModel>>.Failure(ex.ToString());
        }
    }

    public async Task<Result<ReservationResponseModel>> ReserveAsync(ReservationRequestModel model)
    {
        try
        {
            var table = await _unitOfWork.TableRepository.GetByIdAsync(model.TableId);

            if (table == null)
            {
                return Result<ReservationResponseModel>.Failure(
                    $"The table with provided ID {model.TableId} does not exist");
            }

            var workingIntervals =
                await _unitOfWork.WorkingIntervalRepository.GetByTableAndDayAsync(
                    model.TableId,
                    DateOnly.FromDateTime(model.Start));

            if (!IsValidReservationInterval(model.Start, model.End, workingIntervals))
            {
                return Result<ReservationResponseModel>.Failure(
                    "Reservation time is invalid. The reservation must start and end within the restaurant's operating hours and conform to the duration limits set by the establishment.");
            }

            var reservation = new ReservationEntity
            {
                TableId = model.TableId,
                ReservationTime = model.Start,
                DurationInMinutes = (int)(model.End - model.Start).TotalMinutes,
                Status = ReservationStatus.Created
            };

            var createdReservation = await _unitOfWork.ReservationRepository.InsertAsync(reservation);

            var responseModel = _mapper.Map<ReservationResponseModel>(createdReservation);

            return Result<ReservationResponseModel>.Success(responseModel);
        }
        catch (Exception ex)
        {
            return Result<ReservationResponseModel>.Failure(ex.ToString());
        }
    }

    public async Task<Result> CancelReservationAsync(Guid reservationId)
    {
        try
        {
            var reservation = await _unitOfWork.ReservationRepository.GetByIdAsync(reservationId);

            if (reservation == null)
            {
                return Result.Failure($"Reservation with ID {reservationId} does not exist");
            }

            await _unitOfWork.ReservationRepository.CancelReservationAsync(reservationId);

            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure(ex.ToString());
        }
    }

    private List<ReservationRangeResponseModel> CalculateAvailableSlots(
        DateOnly day,
        List<WorkingIntervalEntity> workingIntervals,
        List<ReservationEntity> reservations)
    {
        var availableSlots = new List<ReservationRangeResponseModel>();
        var dayStart = day.ToDateTime(new TimeOnly());

        foreach (var interval in workingIntervals)
        {
            var intervalStart = dayStart + interval.StartTime.ToTimeSpan();
            var intervalEnd = dayStart + interval.EndTime.ToTimeSpan();
            availableSlots.AddRange(FindAvailableSlotsInInterval(intervalStart, intervalEnd, reservations));
        }

        return availableSlots;
    }

    private IEnumerable<ReservationRangeResponseModel> FindAvailableSlotsInInterval(DateTime intervalStart,
        DateTime intervalEnd,
        IEnumerable<ReservationEntity> reservations)
    {
        var slots = new List<ReservationRangeResponseModel>();
        var lastEnd = intervalStart;

        var reservationsInInterval = reservations
            .Where(r => r.ReservationTime >= intervalStart && r.ReservationTime < intervalEnd)
            .OrderBy(r => r.ReservationTime);

        foreach (var reservation in reservationsInInterval)
        {
            if (lastEnd < reservation.ReservationTime)
            {
                slots.Add(new ReservationRangeResponseModel { Start = lastEnd, End = reservation.ReservationTime });
            }

            lastEnd = reservation.ReservationTime.AddMinutes(reservation.DurationInMinutes);
        }

        if (lastEnd < intervalEnd)
        {
            slots.Add(new ReservationRangeResponseModel { Start = lastEnd, End = intervalEnd });
        }
        
        return slots;
    }

    private bool IsValidReservationInterval(
        DateTime start,
        DateTime end,
        List<WorkingIntervalEntity> workingIntervalEntities)
    {
        if (!IsValidReservationDateTime(start) || !IsValidReservationDateTime(end))
        {
            return false;
        }

        foreach (var interval in workingIntervalEntities)
        {
            var intervalStart = interval.OperatingSchedule.Date.ToDateTime(interval.StartTime);
            var intervalEnd = interval.OperatingSchedule.Date.ToDateTime(interval.EndTime);

            if (start >= intervalStart && end <= intervalEnd)
            {
                return true;
            }
        }

        return false;
    }

    private bool IsValidReservationDateTime(DateTime time)
    {
        return
            time.Minute is 0 or 30
            && time is { Second: 0, Millisecond: 0 };
    }
}