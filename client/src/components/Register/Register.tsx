import { Form, Formik } from "formik";
import Input from "../Input/Input";
import "./Register.css";

interface IRegisterForm {
    name: string;
    email: string;
    password: string;
    phoneNumber: string;
    promocode: string | null;
}

const formIntialValues: IRegisterForm = {
    name: "",
    email: "",
    password:"",
    phoneNumber: "",
    promocode: null
}

const Register = () => {

    const onSubmit = (values: IRegisterForm) => {
        setTimeout(() => {
            alert(JSON.stringify(values, null, 2));
            console.log("asd")
        }, 400); 
    }

    return (
        <div className="register-wrapper">
            <div className="register-form-wrapper">
                <h1 className="register-app-name">QRISTO</h1>
                <div className="register-form">
                    <Formik
                        initialValues={formIntialValues}
                        onSubmit={onSubmit}>
                            <Form>
                                <div class-name="register-steps">
                                    <h1 className="register-auth-text">
                                        <span>
                                            Регистрация
                                        </span>
                                    </h1>
                                    <Input name={"name"} type={"text"}/>
                                    <Input name={"email"} type={"email"}/>
                                    <Input name={"password"} type={"password"}/>
                                    <Input name={"phoneNumber"} type={"tel"}/>
                                    <div className="register-promocode">
                                        <span>У меня есть промокод</span>
                                    </div>
                                </div>
                            </Form>
                    </Formik>
                </div>
            </div>
        </div>
    );
}

export default Register;