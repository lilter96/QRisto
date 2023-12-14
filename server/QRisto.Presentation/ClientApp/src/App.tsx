import './App.css';
import {BrowserRouter, Route, Routes} from 'react-router-dom';
import Login from './components/Login/Login';
import Register from './components/Register/Register';

function App() {
    return (
        <div className="App">
            <BrowserRouter>
                <Routes>
                    <Route path="/" element={<Login/>}></Route>
                    <Route path='/login' element={<Login/>}/>
                    <Route path='/register' element={<Register/>}/>
                    <Route path="/restaurants" element={<Register/>}/>
                    <Route path="/booking"/>
                </Routes>
                <footer></footer>
            </BrowserRouter>
        </div>
    );
}

export default App;
