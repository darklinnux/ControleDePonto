import { useState } from "react";

export function useFeedback() {
  const [feedback, setFeedback] = useState({
    open: false,
    severity: 'success',
    message: 'Feedback configurado com sucesso !',
    duration: null, 
  });

  function showFeedback({ severity, message }) {
    setFeedback({
      open: true,
      severity,
      message,
    });
  }

  function onClose(_, reason) {
    if (reason && reason === 'clickaway') {
      return;
    }
    setFeedback({ open: false });
  }

  return { ...feedback, showFeedback, onClose }
}
