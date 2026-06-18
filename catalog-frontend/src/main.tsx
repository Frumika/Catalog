import {BrowserRouter} from 'react-router-dom';
import {createRoot} from 'react-dom/client'
import './app/styles/global.css'
import App from './app/App.tsx'

createRoot(document.getElementById('root')!)
    .render(
        <BrowserRouter>
            <App/>
        </BrowserRouter>,
    );
