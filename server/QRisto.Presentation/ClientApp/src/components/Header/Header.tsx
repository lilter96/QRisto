import React from "react"
import { Link } from "react-router-dom";
import './Header.css';

const IS_LOGGED_IN: boolean = true;

const Header: React.FC = () => {
    return (
        <div className="header-wrapper"> 
            <header className="header">
                <div className="header__appLogo">
                    <span>QRisto</span>
                </div>
                <div className="header__nav">
                    <ul className="header__nav__links">
                        <li className="home">
                            <Link className="homeLink" to="/home">Главная</Link>
                        </li>
                        <li className="configuration">
                            <Link className="configurationLink" to="/configuration">Настройки зала</Link>
                        </li>
                        <li className="logout">
                            <Link className="logoutLink" to="/login">Выход</Link>
                        </li>
                    </ul>
                </div>
            </header>      
        </div>
    );
}

export default Header;