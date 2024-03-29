import React, { useState, useEffect } from "react";
import { IconButton, Box, Tooltip, Button, TextField, Stack } from '@mui/material';
import ModeEditOutlineOutlinedIcon from '@mui/icons-material/ModeEditOutlineOutlined';
import { req } from "../../interceptors";
import { useLoading } from "../../hooks/useLoading";
import { useFeedback } from "../../hooks/useFeedback";
import Loading from "../../components/Loading";
import Datatable from "../../components/Datatables";
import FormDialog from "../../components/FormDialog";
import Autocomplete from "../../components/Autocomplete";
import Feedback from '../../components/Feedback';

const classes = {
  header: { display: 'flex', flexDirection: 'row', justifyContent: 'end', mb: 2 },
  group: { w: '100%', mt: 3 }
}

export default function Colaboradores() {
  // default data and consts
  const feedback = useFeedback();
  const loading = useLoading();
  const [sync, setSync] = useState(false);
  const [titles, setTitles] = useState([]);
  const [profiles, setProfiles] = useState([]);
  const [schedules, setSchedules] = useState([]);
  const [employees, setEmployees] = useState([]);
  // consts to handle action add
  const [showAdd, setShowAdd] = useState(false);
  const [newEmployee, setNewEmployee] = useState({});
  // consts to handle action edit
  const [showEdit, setShowEdit] = useState(false);
  const [employeeToEdit, setEmployeeToEdit] = useState({});
  
  function onCreate() { setShowAdd(true); }
  function onCloseCancel(_, reason) {
    if (reason === 'backdropClick' || reason === 'escapeKeyDown') {
      return
    } else {
      setNewEmployee({});
      setShowAdd(false);
    }
  }

  function onChangeInput(event) {
    const { name, value } = event.target;
    setNewEmployee({ ...newEmployee, [name]: value });
  }

  function onChangeSelect(_, value) {
    const { field, id } = value;
    if (field && id) {
      setNewEmployee({ ...newEmployee, [field]: id });
    } else {
      let newEmployeeCopy = newEmployee;
      delete newEmployeeCopy[field];
      setNewEmployee({ ...newEmployeeCopy });
    }
  }

  async function onSubmit() {
    try {
      await req.post('Employee', newEmployee);
      setNewEmployee({});
      setShowAdd(false);
      setSync(!sync);
      loading.setIsLoading(true);
      feedback.showFeedback({
        severity: 'success',
        message: 'Colaborador(a) adicionado(a) com sucesso!',
      });
    } catch(e) {
      console.log(e);
      feedback.showFeedback({
        severity: 'error',
        message: 'Não foi possível concluir o cadastro do(a) colaborador(a). Tente novamente mais tarde ou entre em contato com o Administrador.'
      });
    }
  }

  async function loadEmployees() {
    try {
      const employees = await req.get('Employee');
      return employees.data;
    } catch(e) {
      console.log(e);
      throw new Error('Não foi possível obter a lista de colaboradores.');
    }
  }

  useEffect(() => {
    async function loadTitles() {
      try {
        const titles = await req.get('Title');
        return titles.data;
      } catch(e) {
        throw new Error('Não foi possível obter a lista de cargos.');
      }
    }

    async function loadProfiles() {
      try {
        const profiles = await req.get('Profile');
        return profiles.data;
      } catch(e) {
        throw new Error('Não foi possível obter a lista de perfis.');
      }
    }

    async function loadSchedules() {
      try {
        const schedules = await req.get('Schedule');
        return schedules.data;
      } catch(e) {
        console.log(e);
        throw new Error('Não foi possível obter a lista de escalas.');
      }
    }

    async function loadDefaultData() {
      try {
        const [titles, profiles, schedules] = await Promise.all([loadTitles(), loadProfiles(), loadSchedules()]);
        setTitles(titles);
        setProfiles(profiles);
        setSchedules(schedules);
      } catch(e) {
        console.log(e);
        feedback.showFeedback({
          severity: 'error',
          message: `${e.message}. Tente novamente mais tarde ou entre em contato com o Administrador`
        });
      }
    }
    loadDefaultData();
  }, []);

  useEffect(() => {
    async function syncData() {
      try {
        const [employees] = await Promise.all([loadEmployees()]);
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
    syncData();
  }, [sync]);

  // handlers to edit...
  const onClickToEditEmployee = (id) => {
    const employee = employees[employees.findIndex(employee => employee.Id === id)];
    const employeeData = {
      id: employee.id,
      name: employee.name,
      registration: employee.registration,
      scheduleId: employee.scheduleId,
      titleId: employee.titleId,
      userId: employee.user?.id,
    }
    setEmployeeToEdit(employeeData);
    setShowEdit(true);
  }

  const onCloseCancelEdit = (_, reason) => {
    if (reason === 'backdropClick' || reason === 'escapeKeyDown') {
      return
    } else {
      setShowEdit(false);
      setEmployeeToEdit({});
    }
  }

  function onChangeInputEdit(event) {
    const { name, value } = event.target;
    setEmployeeToEdit({ ...employeeToEdit, [name]: value });
  }

  function onChangeSelectEdit(_, value) {
    const { field, id } = value;
    if (field && id) {
      setEmployeeToEdit({ ...employeeToEdit, [field]: id });
    } else {
      let employeeToEditCopy = employeeToEdit;
      delete employeeToEditCopy[field];
      setEmployeeToEdit({ ...employeeToEditCopy });
    }
  }

  async function onUpdate() {
    try {
      await req.put('Employee', employeeToEdit);
      setEmployeeToEdit({});
      setShowEdit(false);
      setSync(!sync);
      loading.setIsLoading(true);
      feedback.showFeedback({
        severity: 'success',
        message: 'Dados do colaborador(a) atualizados com sucesso!',
      });
    } catch(e) {
      console.log(e);
      feedback.showFeedback({
        severity: 'error',
        message: 'Não foi possível concluir o processo de atualização dos dados do(a) colaborador(a). Tente novamente mais tarde ou entre em contato com o Administrador.'
      });
    }
  }

  const columns = [
    {
      name: 'name',
      label: 'Nome',
      options: {
       filter: true
      }
    },
    {
      name: 'registration',
      label: 'Matrícula',
      options: {
       filter: true
      }
    },
    {
      name: 'title',
      label: 'Cargo',
      options: { 
        customBodyRender: (param) => param?.name
      }
    },
    {
      name: 'schedule',
      label: 'Escala',
      options: { 
        customBodyRender: (param) => param?.name
      }
    },
    {
      name: 'user',
      label: 'Perfil',
      options: { 
        customBodyRender: (param) => param?.profile?.name
      }
    },
    {
      name: 'Id',
      label: 'Ações',
      options: { 
        customBodyRender: (param) => { ;
          return (
            <div>
              <Tooltip title='Editar Informações' placement='bottom'>
                <IconButton variant='contained' onClick={() => onClickToEditEmployee(param)}>
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
        <Button variant='contained' onClick={onCreate}>Adicionar Colabrador</Button>
      </Box>
      
      { loading.isLoading ? <Loading /> : <Datatable title='Colaboradores' data={employees} columns={columns} /> }
      
      {/* Modal add collaborator */}
      <FormDialog title="Adicionar Colaborador(a)" open={showAdd} onCloseCancel={onCloseCancel} onSubmit={onSubmit}>
        <TextField
            id='name'
            name='name'
            label='Nome'
            variant='outlined'
            sx={{ mt: 2 }}
            autoComplete='off'
            fullWidth
            autoFocus
            InputLabelProps={{ shrink: true }}
            onChange={onChangeInput}
          />
          <Stack direction='row' spacing={2} alignItems='center' sx={classes.group}>
            <TextField
              id='registration'
              name='registration'
              label='Matrícula'
              variant='outlined'
              sx={{ mt: 3 }}
              autoComplete='off'
              fullWidth
              InputLabelProps={{ shrink: true }}
              onChange={onChangeInput}
            />
            <Autocomplete
              label='Cargo'
              data={titles}
              onChange={(_, value) => onChangeSelect(_, { field: 'titleId', ...value })}
            />
          </Stack>
          <Stack direction='row' spacing={2} alignItems='center' sx={classes.group}>
            <Autocomplete
              label='Perfil'
              data={profiles}
              onChange={(_, value) => onChangeSelect(_, { field: 'profileId', ...value })}
            />
            <Autocomplete
              label='Escala'
              data={schedules}
              onChange={(_, value) => onChangeSelect(_, { field: 'scheduleId', ...value })}
            />
          </Stack>
          <Stack direction='row' spacing={2} alignItems='center' sx={classes.group}>
            <TextField
              id='login'
              name='login'
              label='Login'
              variant='outlined'
              autoComplete='off'
              fullWidth
              InputLabelProps={{ shrink: true }}
              onChange={onChangeInput}
            />
            <TextField
              id='password'
              name='password'
              type="password"
              label='Senha'
              variant='outlined'
              autoComplete='off'
              fullWidth
              InputLabelProps={{ shrink: true }}
              onChange={onChangeInput}
            />
          </Stack>
      </FormDialog>

      {/* Modal edit collaborator */}
      <FormDialog title="Editar Colaborador(a)" open={showEdit} editMode onCloseCancel={onCloseCancelEdit} onSave={onUpdate}>
        <TextField
            id='name'
            name='name'
            label='Nome'
            defaultValue={employeeToEdit.name}
            variant='outlined'
            sx={{ mt: 2 }}
            autoComplete='off'
            fullWidth
            autoFocus
            InputLabelProps={{ shrink: true }}
            onChange={onChangeInputEdit}
          />
          <Stack direction='row' spacing={2} alignItems='center' sx={classes.group}>
            <TextField
              id='registration'
              name='registration'
              label='Matrícula'
              defaultValue={employeeToEdit.registration}
              variant='outlined'
              sx={{ mt: 3 }}
              autoComplete='off'
              fullWidth
              InputLabelProps={{ shrink: true }}
              onChange={onChangeInputEdit}
            />
            <Autocomplete
              label='Cargo'
              data={titles}
              defaultValue={titles[titles.findIndex(title => title.id === employeeToEdit.titleId)]}
              onChange={(_, value) => onChangeSelectEdit(_, { field: 'titleId', ...value })}
            />
          </Stack>
          <Stack direction='row' spacing={2} alignItems='center' sx={classes.group}>
            <Autocomplete
              label='Escala'
              data={schedules}
              defaultValue={schedules[schedules.findIndex(schedule => schedule.id === employeeToEdit.scheduleId)]}
              onChange={(_, value) => onChangeSelectEdit(_, { field: 'scheduleId', ...value })}
            />
          </Stack>
      </FormDialog>
    </>
  )
}