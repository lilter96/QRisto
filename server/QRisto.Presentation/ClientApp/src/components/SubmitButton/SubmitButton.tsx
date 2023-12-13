import "./SubmitButton.css";

export interface ISubmitButtonProps {
    text: string;
}

const SubmitButton = ({text} : ISubmitButtonProps) => {
    return (
        <button className="submit-button" type="submit">
            {text}
        </button>
    );
}

export default SubmitButton;