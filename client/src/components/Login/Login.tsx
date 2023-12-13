import { Formik, Form } from "formik";
import "./Login.css";
import { Link } from "react-router-dom";
import Input from "../Input/Input";
import SubmitButton from "../SubmitButton/SubmitButton";

interface ILoginForm {
    userName: string | null;
    password: string | null;
}

const formInitialValues : ILoginForm = {
    userName: "",
    password: ""
};

const Login: React.FC = () => {

    const onSubmit = (values: ILoginForm) => {
        setTimeout(() => {
            alert(JSON.stringify(values, null, 2));
            console.log("asd")
        }, 400); 
    }

    return (
        <div className="login-wrapper">
            <div className="login-form-wrapper">
                <h1 className="app-name">
                    QRISTO
                </h1>
                <div className="login-form">
                    <Formik
                        initialValues={formInitialValues}
                        onSubmit={onSubmit}>
                            <Form>
                                <div className="login-steps">
                                    <h1 className="auth-text">Вход в панель управления</h1>
                                    <Input name={"userName"} type={"text"}/>
                                    <Input name={"password"} type={"password"}/>
                                    <div className="login-actions">
                                        <SubmitButton text="Войти"/>
                                        <Link className="passowrd-recovery-link" to="passwordrecovery">
                                            Забыли пароль?
                                        </Link>
                                    </div>
                                </div>
                            </Form>
                    </Formik>
                </div>
            </div>
        </div>
    );
}

export default Login;