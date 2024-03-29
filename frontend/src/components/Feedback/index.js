import { Snackbar, Alert, Slide } from "@mui/material";

const classes = {
  alert: {
    width: '100%',
    color: '#FFF',
  }
};

export default function Feedback({ open, severity, message, onClose, duration, position }) {
	position = position ? position : { vertical: 'bottom', horizontal: 'right' }
	const { vertical, horizontal } = position;
  return (
    <Snackbar
      open={open ? open : false}
      autoHideDuration={duration ? duration : 5000}
      anchorOrigin={{ vertical, horizontal }}
      onClose={onClose}
      TransitionComponent={Slide}
    >
      <Alert
        variant="filled"
        severity={severity ? severity : ''}
        sx={classes.alert}
        onClose={onClose}
        className='no-border-radius'
      >
        {message ? message : ''}
      </Alert>
    </Snackbar>
  );
};
