import { ErrorMessage, Field, Form, Formik } from "formik";

interface ILoginForm {
    userName: string | null;
    password: string | null;
}

interface ILoginFormErrors {
    userName: string | null;
    password: string | null;
}

const Profile: React.FC = () => {
    return (
        <div className="login-wrapper">
            <div className="login">
                <Formik
                    initialValues={{ userName: '', password: ''}}
                    validate={(values:ILoginForm) => {
                        const errors:ILoginFormErrors = { userName: null, password: null};
                        if (!values.userName) {
                        errors.userName = 'Required';
                        } else if (
                        !/^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,}$/i.test(values.userName)
                        ) {
                        errors.userName = 'Invalid email address';
                        }
                        return errors;
                    }}
                    onSubmit={(values: ILoginForm, { setSubmitting }):void => {
                        setTimeout(() => {
                            alert(JSON.stringify(values, null, 2));
                            setSubmitting(false);
                        }, 400);    
                    }}>
                        {({ isSubmitting }) => (
                            <Form>
                            <Field type="text" name="userName" />
                            <ErrorMessage name="email" component="div" />
                            <Field type="password" name="password" />
                            <ErrorMessage name="password" component="div" />
                            <button type="submit" disabled={isSubmitting}>
                                Submit
                            </button>
                            </Form>
                        )}
                </Formik>
            </div>
        </div>
    );
}

export default Profile;