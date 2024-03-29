import React from 'react';
import { Autocomplete as MUIAutocomplete, TextField } from '@mui/material';

export default function Autocomplete({ label, value, defaultValue, data, onChange, size, sx }) {
  return (
    <MUIAutocomplete
      sx={sx ? sx : { marginTop: '10px' }}
      size={size ? size : 'medium'}
      fullWidth
      onChange={onChange}
      options={data}
      value={value ?? defaultValue}
      defaultValue={defaultValue}
      getOptionLabel={(option) => `${option.name}`}
      isOptionEqualToValue={(option, value) => option === value}
      renderInput={(params) => (
        <TextField {...params} label={label} InputLabelProps={{ shrink: true }} />
      )}
    />
  );
}
