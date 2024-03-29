import React from 'react';
import Button from '@mui/material/Button';
import Dialog from '@mui/material/Dialog';
import DialogActions from '@mui/material/DialogActions';
import DialogContent from '@mui/material/DialogContent';
import DialogTitle from '@mui/material/DialogTitle';

const classes = {
  main: {
    backgroundColor: '#1976D2',
    color: '#FFF',
    mb: 2
  },
  content: {
    mt: 1
  }
}

export default function FormDialog({ title, open, editMode, previewMode, children, onSubmit, onSave, onCloseCancel }) {
  return (
    <Dialog open={open} onClose={onCloseCancel} fullWidth maxWidth="md">
      <DialogTitle sx={classes.main}>{ title }</DialogTitle>
      <DialogContent>
        <div style={classes.content}>
          { children }
        </div>
      </DialogContent>
      <DialogActions>
        { previewMode ? null : <Button color="error" onClick={onCloseCancel}>Cancelar</Button> }
        {
          editMode ? <Button onClick={onSave}>Salvar</Button> : null
        }
        {
          previewMode ? <Button color="secondary" onClick={onCloseCancel}>Fechar Visualização</Button> : null
        }
        {
          !editMode && !previewMode ? <Button onClick={onSubmit}>Adicionar</Button> : null
        }
      </DialogActions>
    </Dialog>
  );
}
