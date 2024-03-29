import React, { useState, useEffect } from "react";
import moment from "moment";
import 'moment/locale/pt-br';
import { Box, Button, Stack, TextField, Tooltip, IconButton } from '@mui/material';
import ModeEditOutlineOutlinedIcon from '@mui/icons-material/ModeEditOutlineOutlined';
import { useAccount } from '../../hooks/useAccount';
import { useFeedback } from "../../hooks/useFeedback";
import { useLoading } from "../../hooks/useLoading";
import { req } from "../../interceptors";
import Loading from '../../components/Loading';
import FormDialog from '../../components/FormDialog';
import Autocomplete from '../../components/Autocomplete';
import Feedback from '../../components/Feedback';
import Datatable from "../../components/Datatables";

const classes = {
  header: { display: 'flex', flexDirection: 'row', justifyContent: 'end', mb: 2 },
  group: { w: '100%', mt: 3 }
}

export default function Pontos() {
  const account = useAccount();
  const feedback = useFeedback();
  const loading = useLoading();
  const [sync, setSync] = useState(false);
  const [employees, setEmployees] = useState([]);
  const [markings, setMarkings] = useState([]);
  const [employeeMarkings, setEmployeeMarkings] = useState([]);
  const [showAdd, setShowAdd] = useState(false);
  const [newMarking, setNewMarking] = useState({});
  const [showEdit, setShowEdit] = useState(false);
  const [employeeMarkingToEdit, setEmployeeMarkingToEdit] = useState({});

  const onCreate = () => setShowAdd(true);
  const onCancel = (_, reason) => {
    if (reason === 'backdropClick' || reason === 'escapeKeyDown') {
      return
    } else {
      setShowAdd(false);
    }
  };

  function onChangeSelect(_, value) {
    const { field, id } = value;
    if (field && id) {
      setNewMarking({ ...newMarking, [field]: id });
    } else {
      let copy = newMarking;
      delete copy[field];
      setNewMarking(copy);
    }
  }

  function onChangeInput(event) {
    const { name, value } = event.target;
    setNewMarking({ ...newMarking, [name]: value });
  }

  async function onSubmit() {
    try {
      if (account && account.profile_id === "2") {
        newMarking.employeeId = account.account_id
      };
      await req.post('EmployeeMarking', newMarking);
      setNewMarking({});
      setShowAdd(false);
      loading.setIsLoading(true);
      setSync(!sync);
      feedback.showFeedback({
        severity: 'success',
        message: 'Registro adicionado com sucesso!',
      });
    } catch(e) {
      console.log(e);
      feedback.showFeedback({
        severity: 'error',
        message: 'Não foi possível concluir o cadastro do(a) colaborador(a). Tente novamente mais tarde ou entre em contato com o Administrador.'
      });
    }
  }

  useEffect(() => {
    const loadEmployees = async () => {
      try {
        const response = await req.get('Employee');
        return response.data;
      } catch(e) {
        console.log(e);
        feedback.showFeedback({
          severity: 'error',
          message: `${e.message}. Tente novamente mais tarde ou entre em contato com o Administrador`
        });
      }
    }

    const loadMarkings = async () => {
      try {
        const response = await req.get('Marking');
        return response.data;
      } catch(e) {
        console.log(e);
        feedback.showFeedback({
          severity: 'error',
          message: `${e.message}. Tente novamente mais tarde ou entre em contato com o Administrador`
        });
      }
    }

    const loadDefaultData = async () => {
      try {
        const [markings, employees] = await Promise.all([loadMarkings(), loadEmployees()]);
        setMarkings(markings);
        setEmployees(employees);
        loading.setIsLoading(false);
      } catch(e) {
        console.log(e);
        feedback.showFeedback({
          severity: 'error',
          message: `${e.message}. Tente novamente mais tarde ou entre em contato com o Administrador`
        });
      }
    }
    loadDefaultData();
  }, [loading, feedback]);

  useEffect(() => {
    const loadEmployeeMarkings = async () => {
      try {
        if (account) {
          let employeeMarkings;
          switch(account.profile_id) {
            case "1":
              employeeMarkings = await req.get('EmployeeMarking');
              setEmployeeMarkings(employeeMarkings.data);
              loading.setIsLoading(false);
              return;
            case "2":
              employeeMarkings = await req.get(`EmployeeMarking/${account.account_id}`);
              setEmployeeMarkings(employeeMarkings.data);
              loading.setIsLoading(false);
              return
            default:
              break;
          }
        }
      } catch(e) {
        console.log(e);
        feedback.showFeedback({
          severity: 'error',
          message: `${e.message}. Tente novamente mais tarde ou entre em contato com o Administrador`
        });
      }
    }
    loadEmployeeMarkings();
  }, [sync, account, loading, feedback]);

  const onCloseCancelEdit = (_, reason) => {
    if (reason === 'backdropClick' || reason === 'escapeKeyDown') {
      return
    } else {
      setEmployeeMarkingToEdit({});
      setShowEdit(false);
    }
  }

  const onClickEdit = async (id) => {
    const selectedEmployeeMarking = employeeMarkings[employeeMarkings.findIndex(em => em.id === id)];
    setEmployeeMarkingToEdit(selectedEmployeeMarking);
    setShowEdit(true);
  }

  function onChangeSelectEdit(_, value) {
    const { field, id } = value;
    if (field && id) {
      setEmployeeMarkingToEdit({ ...employeeMarkingToEdit, [field]: id });
    } else {
      let copy = employeeMarkingToEdit;
      delete copy[field];
      setEmployeeMarkingToEdit(copy);
    }
  }

  function onChangeInputEdit(event) {
    const { name, value } = event.target;
    setEmployeeMarkingToEdit({ ...employeeMarkingToEdit, [name]: value });
  }

  async function onSave() {
    try {
      await req.put('EmployeeMarking', employeeMarkingToEdit);
      setShowEdit(false);
      setEmployeeMarkingToEdit({});
      loading.setIsLoading(true);
      setSync(!sync);
      feedback.showFeedback({
        severity: 'success',
        message: 'Registro atualizado com sucesso!',
      });
    } catch(e) {
      console.log(e);
      feedback.showFeedback({
        severity: 'error',
        message: `${e.message}. Tente novamente mais tarde ou entre em contato com o Administrador`
      });
    }
  }

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
    },
    {
      name: 'id',
      label: 'Ações',
      options: { 
        customBodyRender: (param) => {
          return (
            <div>
              <Tooltip title='Editar Informações' placement='bottom'>
                <IconButton variant='contained' onClick={() => onClickEdit(param)}>
                  <ModeEditOutlineOutlinedIcon />
                </IconButton>
              </Tooltip>
            </div>
          )
        }
      }
    }
  ];

  return (
    <>
      <Feedback open={feedback.open} severity={feedback.severity} message={feedback.message} onClose={feedback.onClose} />
      <Box sx={classes.header}>
        <Button variant='contained' onClick={onCreate}>Adicionar Registro</Button>
      </Box>
      { loading.isLoading ? <Loading /> : <Datatable title='Registros de Ponto' data={employeeMarkings} columns={columns} /> }

      {/* Modal add */}
      <FormDialog open={showAdd} title="Registro de Ponto" onCloseCancel={onCancel} onSubmit={onSubmit}>
        { 
          parseInt(account?.profile_id, 10) === 1 &&
          <Autocomplete
            label='Colaborador(a)'
            data={employees}
            onChange={(_, value) => onChangeSelect(_, { field: 'employeeId', ...value })}
          />
        }
        <Stack direction='row' spacing={2} alignItems='center' sx={classes.group}>
          <Autocomplete
            label='Tipo de Registro'
            data={markings}
            onChange={(_, value) => onChangeSelect(_, { field: 'markingId', ...value })}
          />
          <TextField
            id='time'
            name='dateTime'
            type="datetime-local"
            label='Dia e Horário'
            variant='outlined'
            sx={{ mb: 3, mt: 2 }}
            autoComplete='off'
            fullWidth
            InputLabelProps={{ shrink: true }}
            onChange={onChangeInput}
          />
        </Stack>
      </FormDialog>

      {/* Modal edit */}
      <FormDialog open={showEdit} title="Editar Registro de Ponto" editMode onCloseCancel={onCloseCancelEdit} onSave={onSave}>
        { 
          parseInt(account?.profile_id, 10) === 1 &&
          <Autocomplete
            label='Colaborador(a)'
            data={employees}
            defaultValue={employees[employees.findIndex(employee => employee.id === employeeMarkingToEdit.employeeId)]}
            onChange={(_, value) => onChangeSelectEdit(_, { field: 'employeeId', ...value })}
          />
        }
        <Stack direction='row' spacing={2} alignItems='center' sx={classes.group}>
          <Autocomplete
            label='Tipo de Registro'
            data={markings}
            defaultValue={markings[markings.findIndex(marking => marking.id === employeeMarkingToEdit.markingId)]}
            onChange={(_, value) => onChangeSelectEdit(_, { field: 'markingId', ...value })}
          />
          <TextField
            id="time"
            name="dateTime"
            type="datetime-local"
            label="Dia e Horário"
            variant="outlined"
            value={moment(employeeMarkingToEdit.dateTime).format("YYYY-MM-DTHH:mm")}
            sx={{ mb: 3, mt: 2 }}
            autoComplete="off"
            fullWidth
            InputLabelProps={{ shrink: true }}
            onChange={onChangeInputEdit}
          />
        </Stack>
      </FormDialog>
    </>
  )
}