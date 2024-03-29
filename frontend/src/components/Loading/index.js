import * as React from 'react';
import { Box, CircularProgress } from '@mui/material';

export default function Loading() {
  const style = {
    width: '100%',
    display: 'flex',
    flexDirection: 'row',
    justifyContent: 'center',
    pt: 2,
    pb: 2
  }
  return (
    <Box sx={style}>
      <CircularProgress />
    </Box>
  );
}
