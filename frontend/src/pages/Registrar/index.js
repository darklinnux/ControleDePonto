import React, { useState } from "react";
import { req } from '../../interceptors';
import { useFeedback } from "../../hooks/useFeedback";
import {
  Grid,
  Box,
  FormControl,
  TextField,
  Button,
} from '@mui/material';
import Feedback from '../../components/Feedback';
import { useNavigate } from "react-router-dom";
import { jwtDecode } from "jwt-decode";

const classes = {
  main: {
    minHeight: '100vh !important',
  },
  content: {
    display: 'flex',
    flexDirection: 'column',
    alignItens: 'center',
    justifyContent: 'center',
    height: '100vh',
    width: { xs: '90%', sm: '100%' },
    padding: { xs: '5%' },
  },
  contentHeader: {
    textAlign: 'center',
    mb: 3,
  },
  formControl: {
    width: '100%',
    mb: 3,
  },
  footer: {
    width: '100%',
    display: 'flex',
    flexDirection: 'row',
    justifyContent: 'flex-end',
    marginTop: '20px'
  }
}

export default function Registrar() {
  const feedback = useFeedback();
  const navigate = useNavigate();
  const [register, setRegister] = useState({ profileId: 1 });

  const onChangeInput = (event) => {
    const { name, value } = event.target;
    setRegister({ ...register, [name]: value });
  }

  const onSubmit = async () => {
    try {
      if (register.password && (register.password === register.password_confirmation)) {
        const action = await req.post('User/registrer', register);
        const { token } = action.data;
        localStorage.setItem('token', token);
        const dataFromToken = jwtDecode(token);
        if (dataFromToken) {
          switch (dataFromToken.profileid) {
            case "1":
              navigate('/colaboradores');
              break;
            case "2":
              navigate('/registros-de-ponto');
              break;
            default:
              throw new Error('Autenticação não está funcionando!');
          }
        }
      } else {
        feedback.showFeedback({
          severity: 'error',
          message: 'As senhas não são iguais. Insira novamente!',
        });
      }
    } catch(e) {
      console.log(e);
      feedback.showFeedback({
        severity: 'error',
        message: 'Tente novamente mais tarde ou entre em contato com o Administrador',
      });
    }
  }

  return (
    <>
      <Grid container sx={classes.main}>
        <Feedback open={feedback.open} severity={feedback.severity} message={feedback.message} onClose={feedback.onClose} />
        <Grid item sm={1} md={2} lg={3} />
        <Grid item xs={12} sm={10} md={8} lg={6} sx={classes.content}>
          <Box sx={classes.contentHeader}>
            { /* logo here... */ }
          </Box>
          <Box>
            <FormControl sx={classes.formControl} variant="outlined">
              <TextField
                name="login"
                label="Login"
                fullWidth
                autoFocus
                autoComplete="off"
                InputLabelProps={{ shrink: true }}
                onChange={onChangeInput}
              />
            </FormControl>
            <FormControl sx={classes.formControl} variant="outlined">
              <TextField
                name="password"
                label="Senha"
                fullWidth
                type="password"
                InputLabelProps={{ shrink: true }}
                onChange={onChangeInput}
              />
            </FormControl>
            <FormControl sx={classes.formControl} variant="outlined">
              <TextField
                name="password_confirmation"
                label="Confirmação da Senha"
                fullWidth
                type="password"
                InputLabelProps={{ shrink: true }}
                onChange={onChangeInput}
              />
            </FormControl>
            <Button
              className="degrade-primary"
              variant="contained"
              size="large"
              disableElevation
              fullWidth
              onClick={onSubmit}
            >
              Registrar
            </Button>
          </Box>
        </Grid>
        <Grid item sm={1} md={2} lg={3} />
      </Grid>
    </>
  );
}
