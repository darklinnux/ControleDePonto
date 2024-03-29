import React, { useState } from 'react';
import { useLocation, useNavigate } from 'react-router-dom';
import AppBar from '@mui/material/AppBar';
import Box from '@mui/material/Box';
import CssBaseline from '@mui/material/CssBaseline';
import Divider from '@mui/material/Divider';
import Drawer from '@mui/material/Drawer';
import IconButton from '@mui/material/IconButton';
import List from '@mui/material/List';
import ListItem from '@mui/material/ListItem';
import ListItemButton from '@mui/material/ListItemButton';
import ListItemIcon from '@mui/material/ListItemIcon';
import ListItemText from '@mui/material/ListItemText';
import MenuIcon from '@mui/icons-material/Menu';
import Toolbar from '@mui/material/Toolbar';
import Typography from '@mui/material/Typography';
import ExitToAppIcon from '@mui/icons-material/ExitToApp';
import AccountCircleOutlinedIcon from '@mui/icons-material/AccountCircleOutlined';
import AccessTimeOutlinedIcon from '@mui/icons-material/AccessTimeOutlined';
import ContentPasteSearchOutlinedIcon from '@mui/icons-material/ContentPasteSearchOutlined';
import { useAccount } from '../../hooks/useAccount';

const drawerWidth = 240;

const classes = {
  content: {
    flexGrow: 1,
    p: 3,
    width: { sm: `calc(100% - ${drawerWidth}px)` },
    minHeight: '100vh',
    backgroundColor: '#F8F9FA'
  }
}

function ResponsiveDrawer({ children, window }) {
  const container = window !== undefined ? () => window().document.body : undefined;
  const location = useLocation();
  const navigate = useNavigate();
  const account = useAccount();
  const [mobileOpen, setMobileOpen] = useState(false);
  const [isClosing, setIsClosing] = useState(false);
  const [pathname, setPathname] = useState(location.pathname);

  const handleDrawerClose = () => {
    setIsClosing(true);
    setMobileOpen(false);
  };

  const handleDrawerTransitionEnd = () => {
    setIsClosing(false);
  };

  const handleDrawerToggle = () => {
    if (!isClosing) {
      setMobileOpen(!mobileOpen);
    }
  };

  const handleClickMenu = (path) => {
    setPathname(path);
    navigate(path);
  }

  const options = [
    {
      name: 'Colaboradores',
      url: '/colaboradores',
      icon: <AccountCircleOutlinedIcon />,
      acl: 1,
    },
    {
      name: `Registros de Ponto`,
      url: '/registros-de-ponto',
      icon: <AccessTimeOutlinedIcon />,
      acl: 2
    },
    {
      name: `Relatórios`,
      url: '/relatorios',
      icon: <ContentPasteSearchOutlinedIcon />,
      acl: 1
    },
  ];

  const drawer = (
    <div>
      <Toolbar/>
      <Divider />
      <List>
        {
          options.map((option, index) => {
            if (option.acl >= account.profile_id) {
              return (
                <ListItem key={index} disablePadding>
                  <ListItemButton selected={pathname === option.url} onClick={() => handleClickMenu(option.url)}>
                    <ListItemIcon sx={{ minWidth: 40 }}>
                      {option.icon}
                    </ListItemIcon>
                    <ListItemText primary={option.name} />
                  </ListItemButton>
                </ListItem>
              )
            }
            return null
          })
        }
      </List>
    </div>
  );

  return (
    <Box sx={{ display: 'flex' }}>
      <CssBaseline />
      <AppBar
        elevation={0}
        position="fixed"
        sx={{
          width: { sm: `calc(100% - ${drawerWidth}px)` },
          ml: { sm: `${drawerWidth}px` },
        }}
      >
        <Toolbar sx={{ display: 'flex', flexDirection: 'row', justifyContent: 'space-between' }}>
          <IconButton
            color="inherit"
            aria-label="open drawer"
            edge="start"
            onClick={handleDrawerToggle}
            sx={{ mr: 2, display: { sm: 'none' } }}
          >
            <MenuIcon />
          </IconButton>
          <Typography variant="h6" noWrap component="div">
            Olá {account?.login},
          </Typography>
          <IconButton aria-label="logout" sx={{ color: 'white' }} onClick={() => navigate('/')}>
            <ExitToAppIcon />
          </IconButton>
        </Toolbar>
      </AppBar>
      <Box
        component="nav"
        sx={{ width: { sm: drawerWidth }, flexShrink: { sm: 0 } }}
        aria-label="mailbox folders"
      >
        <Drawer
          container={container}
          variant="temporary"
          open={mobileOpen}
          onTransitionEnd={handleDrawerTransitionEnd}
          onClose={handleDrawerClose}
          ModalProps={{
            keepMounted: true,
          }}
          sx={{
            display: { xs: 'block', sm: 'none' },
            '& .MuiDrawer-paper': { boxSizing: 'border-box', width: drawerWidth },
          }}
        >
          {drawer}
        </Drawer>
        <Drawer
          variant="permanent"
          sx={{
            display: { xs: 'none', sm: 'block' },
            '& .MuiDrawer-paper': { boxSizing: 'border-box', width: drawerWidth },
          }}
          open
        >
          {drawer}
        </Drawer>
      </Box>
      <Box
        component="main"
        sx={classes.content}
      >
        <Toolbar />
        { children }
      </Box>
    </Box>
  );
}

export default ResponsiveDrawer;
