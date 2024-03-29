import React, { useState, useEffect } from "react";
import moment from "moment";
import { Grid, Button, TextField } from '@mui/material';
import { req } from '../../interceptors';
import { useLoading } from '../../hooks/useLoading';
import { useFeedback } from "../../hooks/useFeedback";
import Feedback from "../../components/Feedback";
import Autocomplete from "../../components/Autocomplete";
import Loading from '../../components/Loading';
import Datatable from "../../components/Datatables";

export default function Relatorio() {
  const loading = useLoading();
  const feedback = useFeedback();
  const [employees, setEmployees] = useState([]);
  const [employeeToFind, setEmployeeToFind] = useState(null);
  const [queryParams, setQueryParams] = useState({ initialdate: '', finaldate: '' });
  const [resultSearch, setResultSearch] = useState([]);

  function onChangeSelect(_, value) {
    const { id } = value;
    if (id) {
      setEmployeeToFind(id);
    } else {
      setEmployeeToFind(null);
    }
  }

  function onChangeInput(event) {
    const { name, value } = event.target;
    setQueryParams({...queryParams, [name]: value });
  }

  async function onSearch() {
    try {
      if (!employeeToFind) {
        throw new Error('A pesquisa deve ser realizada com pelo menos um colaborador(a) selecionado.');
      } else {
        loading.setIsLoading(true);
        if (queryParams.initialdate && queryParams.finaldate) {
          const result = await req.get(`EmployeeMarking/${employeeToFind}?initialdate=${queryParams.initialdate}&finaldate=${queryParams.finaldate}`);
          setResultSearch(result.data);
        } else {
          const result = await req.get(`EmployeeMarking/${employeeToFind}`);
          setResultSearch(result.data);
        }
        loading.setIsLoading(false);
      }
    } catch(e) {
      console.log(e);
      feedback.showFeedback({
        severity: 'error',
        message: `${e.message}. Falha ao obter os dados do relatório. Tente novamente mais tarde ou entre em contato com o Administrador.`
      });
    }
  }

  useEffect(() => {
    const loadEmployees = async () => {
      try {
        const emp = await req.get('Employee');
        setEmployees(emp.data);
      } catch(e) {
        console.log(e);
      }
    }
    loadEmployees();
    loading.setIsLoading(false);
  }, [loading]);

  const columns = [
    {
      name: 'employee',
      label: 'Nome',
      options: {
       filter: true,
       customBodyRender: (param) => param?.name
      }
    },
    {
      name: 'marking',
      label: 'Tipo de Registro',
      options: {
        filter: true,
        customBodyRender: (param) => param?.name
      }
    },
    {
      name: 'dateTime',
      label: 'Data',
      options: {
        filter: true,
        customBodyRender: (param) => moment(param).locale('pt-br').format('D/MM/YYYY')
      }
    },
    {
      name: 'dateTime',
      label: 'Horário',
      options: {
        filter: true,
        customBodyRender: (param) => moment(param).locale('pt-br').format('HH:mm')
      }
    }
  ];

  return (
    <>
      <Feedback open={feedback.open} severity={feedback.severity} message={feedback.message} onClose={feedback.onClose} />
      <Grid container direction="row" justifyContent="center" alignItems="center" spacing={2}>
        <Grid item xs={8} sx={{ display: 'flex', flexDirection: 'row', justifyContent: 'start', mb: 2 }}>
          <Autocomplete
            sx={{ maxWidth: '60% !important' }}
            label='Colaborador(a)'
            data={employees}
            autoFocus
            onChange={(_, value) => onChangeSelect(_, value)}
          />
          &nbsp;&nbsp;&nbsp;
          <TextField
            sx={{ minWidth: '20% !important' }}
            id="initialdate"
            name="initialdate"
            label='Início'
            InputLabelProps={{ shrink: true }}
            type="date"
            onChange={onChangeInput}
          />
          &nbsp;&nbsp;&nbsp;
          <TextField
            sx={{ minWidth: '20% !important' }}
            id="finaldate"
            name="finaldate"
            label='Termino'
            InputLabelProps={{ shrink: true }}
            type="date"
            onChange={onChangeInput}
          />
        </Grid>
        <Grid item xs={4} sx={{ display: 'flex', flexDirection: 'row', justifyContent: 'end', mb: 2 }}>
          <Button variant='contained' onClick={onSearch}>Pesquisar</Button>
        </Grid>
      </Grid>
      { loading.isLoading && <Loading /> }
      { !loading.isLoading && resultSearch.length >= 1 ? <Datatable title="Relatório de Pontos" data={resultSearch} columns={columns} /> : <p>Nenhum resultado encontrado.</p> }
    </>
  );
}