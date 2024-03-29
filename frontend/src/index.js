import React from 'react';
import ReactDOM from 'react-dom/client';
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import { ThemeProvider } from '@mui/material';
import { Theme } from './theme';
import './index.css';
import '@fontsource/roboto/300.css';
import '@fontsource/roboto/400.css';
import '@fontsource/roboto/500.css';
import '@fontsource/roboto/700.css';

import Menu from './components/Menu';
import Login from './pages/Login';
import Colaboradores from './pages/Colaborador';
import Pontos from './pages/Pontos';
import Relatorio from './pages/Relatorio';
import Registrar from './pages/Registrar';

function CustomElement({ page }) {
  return (
    <Menu>
      { page }
    </Menu>
  );
}


const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
  <ThemeProvider theme={Theme}>
    <BrowserRouter>
        <Routes>
          <Route path='/' exact={true} element={<Login />} />
          <Route path='/registrar' exact={true} element={<Registrar />} />
          <Route path='/colaboradores' exact={true} element={<CustomElement page={<Colaboradores />}/> } />
          <Route path='/registros-de-ponto' exact={true} element={<CustomElement page={<Pontos />}/> } />
          <Route path='/relatorios' exact={true} element={<CustomElement page={<Relatorio />}/> } />
        </Routes>
    </BrowserRouter>
  </ThemeProvider>
);