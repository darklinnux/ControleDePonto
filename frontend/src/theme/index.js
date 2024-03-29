import { createTheme } from '@mui/material/styles';

export const Theme = createTheme({
  palette: {
		mode: 'light',
		success: {
			main: '#4CAF50',
			contrastText: '#fff'
		},
		error: {
			main: '#F44336',
			contrastText: '#fff'
		},
		warning: {
      main: '#E3D026',
      contrastText: '#fff',
    },
	},
	typography: {
		htmlFontSize: 15,
		fontSize: 13,
		fontWeightLight: 300,
		fontWeightRegular: 400,
		fontWeightMedium: 500,
		fontWeightBold: 700,
	},
	components: {
		MUIDataTable: {
			styleOverrides:{
				root: {
					borderTop: '1px solid #dee2e6 !important',
					borderLeft: '1px solid #dee2e6 !important',
					borderRight: '1px solid #dee2e6 !important',
					borderRadius: '0 !important',
        },
        paper: {
          boxShadow: "none !important",
        }
			},
    },
		MuiTableCell: {
			styleOverrides: {
				root: {
					padding: '10px !important',
					textAlign: 'center !important'
				}
			}
		},
	}
});
