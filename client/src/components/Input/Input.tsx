import { Field, ErrorMessage } from "formik";
import "./Input.css";

export interface IInputProps {
    type: string;
    name: string;
}

const Input = ({type, name} : IInputProps) => {
    return (
        <div className="form-line">
            <div className="input-field">
                <Field type={type} name={name} />
                <ErrorMessage name={name} component="div" />
            </div>
        </div>
    );
}

export default Input;