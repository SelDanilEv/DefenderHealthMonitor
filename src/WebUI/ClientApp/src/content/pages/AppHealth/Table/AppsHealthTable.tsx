import { FC, ChangeEvent, useState } from 'react';
import PropTypes from 'prop-types';
import {
  Tooltip,
  Divider,
  Box,
  FormControl,
  Card,
  IconButton,
  Table,
  TableBody,
  TableCell,
  TableHead,
  TablePagination,
  TableRow,
  TableContainer,
  Typography,
  useTheme,
  CardHeader,
  TextField,
  Grid
} from '@mui/material';
import EditTwoToneIcon from '@mui/icons-material/EditTwoTone';
import DeleteTwoToneIcon from '@mui/icons-material/DeleteTwoTone';
import { useNavigate } from 'react-router';

import DeleteAppHealthDialog from '../Delete/DeleteAppHealthDialog';

import SuperAdminRoleLable from 'src/components/Label/RoleLabels/SuperAdmin';
import AdminRoleLable from 'src/components/Label/RoleLabels/Admin';
import { AppHealth } from 'src/models/app_health';


interface AppsHealthTableProps {
  className?: string;
  appsHealth: AppHealth[];
  setNewAppHealthList: Function;
}

interface Filters {
  key?: string;
}

const getRoleLabels = (userRoles: string[]) => {
  // const map = {
  //   SuperAdmin: {
  //     label: (<SuperAdminRoleLable />)
  //   },
  //   Admin: {
  //     label: (<AdminRoleLable />)
  //   },
  //   AppHealth: {
  //     label: (<AppUserLable />)
  //   }
  // };

  // return userRoles.map((role) => {

  //   const { label }: any = map[role];

  //   return (<Grid mr={0.5} display="inline-block" key={role}>{label}</Grid>);
  // });
};

const applyFilters = (
  appsHealth: AppHealth[],
  filters: Filters
): AppHealth[] => {
  return appsHealth.filter((user) => {
    let matches = true;

    if (filters.key &&
      !user.id.includes(filters.key) &&
      !user.url.includes(filters.key) &&
      !user.status.includes(filters.key) &&
      !user.name.includes(filters.key)) {
      matches = false;
    }

    return matches;
  });
};

const applyPagination = (
  appsHealth: AppHealth[],
  page: number,
  limit: number
): AppHealth[] => {
  return appsHealth.slice(page * limit, page * limit + limit);
};

const AppsHealthTable: FC<AppsHealthTableProps> = ({ appsHealth: appsHealth, setNewAppHealthList: setNewAppHealthList }) => {

  //Delete user

  const [openDeleteAppHealthDialog, setOpenDeleteAppHealthDialog] = useState(false);
  const [deleteAppHealthId, setDeleteAppHealthId] = useState("");

  const handleOpenDeleteDialog = (value) => {
    setDeleteAppHealthId(value);
    setOpenDeleteAppHealthDialog(true);
  };

  const handleCloseDeleteDialog = () => {
    setOpenDeleteAppHealthDialog(false);
  };

  const handleSuccessDeleting = () => {
    setNewAppHealthList(appsHealth.filter(u => u.id !== deleteAppHealthId));
  }

  //-----------

  //Table settings

  const [page, setPage] = useState<number>(0);

  const [limit, setLimit] = useState<number>(5);

  const [filters, setFilters] = useState<Filters>({
    key: ""
  });

  const handleFilterChange = (e: ChangeEvent<HTMLInputElement>): void => {
    let value = e.target.value;

    setFilters((prevFilters) => ({
      ...prevFilters,
      key: value
    }));
  };

  const handlePageChange = (event: any, newPage: number): void => {
    setPage(newPage);
  };

  const handleLimitChange = (event: ChangeEvent<HTMLInputElement>): void => {
    setLimit(parseInt(event.target.value));
  };

  const filteredAppsHealth = applyFilters(appsHealth, filters);

  const paginatedAppsHealth = applyPagination(
    filteredAppsHealth,
    page,
    limit
  );

  //-----------


  const theme = useTheme();
  const navigate = useNavigate();

  return (
    <>
      <Card>
        <CardHeader
          action={
            <Box width={150}>
              <FormControl fullWidth variant="outlined">
                <TextField
                  onChange={handleFilterChange}
                  label="Filter"
                  value={filters.key}
                />
              </FormControl>
            </Box>
          }
          title="AppsHealth"
        />
        <Divider />
        <TableContainer>
          <Table>
            <TableHead>
              <TableRow>
                <TableCell>Name</TableCell>
                <TableCell>Method</TableCell>
                <TableCell>Url</TableCell>
                <TableCell align="center">Status</TableCell>
                <TableCell align="right">Last update date</TableCell>
              </TableRow>
            </TableHead>
            <TableBody>
              {paginatedAppsHealth.map((appHealth) => {
                return (
                  <TableRow
                    hover
                    key={appHealth.id}
                  >
                    <TableCell>
                      <Typography
                        variant="body1"
                        fontWeight="bold"
                        color="text.primary"
                        gutterBottom
                        noWrap
                      >
                        {appHealth.name}
                      </Typography>
                    </TableCell>
                    <TableCell>
                      <Typography
                        variant="body1"
                        fontWeight="bold"
                        color="text.primary"
                        gutterBottom
                        noWrap
                      >
                        {appHealth.method}
                      </Typography>
                    </TableCell>
                    <TableCell>
                      <Typography
                        variant="body1"
                        fontWeight="bold"
                        color="text.primary"
                        gutterBottom
                        noWrap
                      >
                        {appHealth.url}
                      </Typography>
                    </TableCell>
                    <TableCell align="center">
                      <Typography
                        variant="body1"
                        fontWeight="bold"
                        color="text.primary"
                        gutterBottom
                        noWrap
                      >
                        {appHealth.status}
                      </Typography>
                    </TableCell>
                    <TableCell align="center">
                      <Typography
                        variant="body1"
                        fontWeight="bold"
                        color="text.primary"
                        gutterBottom
                        noWrap
                      >
                        {appHealth.lastUpdateDate}
                      </Typography>
                    </TableCell>
                    <TableCell sx={{ paddingLeft: 1 }} align="center">
                      <Tooltip title="Edit" arrow>
                        <IconButton
                          sx={{
                            '&:hover': {
                              background: theme.colors.primary.lighter
                            },
                            color: theme.palette.primary.main
                          }}
                          color="inherit"
                          size="small"
                          onClick={() => navigate("/appsHealth/edit", { state: { user: appHealth } })}
                        >
                          <EditTwoToneIcon fontSize="small" />
                        </IconButton>
                      </Tooltip>
                      <Tooltip title="Delete" arrow>
                        <IconButton
                          sx={{
                            '&:hover': { background: theme.colors.error.lighter },
                            color: theme.palette.error.main
                          }}
                          color="inherit"
                          size="small"
                          onClick={() => handleOpenDeleteDialog(appHealth.id)}
                        >
                          <DeleteTwoToneIcon fontSize="small" />
                        </IconButton>
                      </Tooltip>
                    </TableCell>
                  </TableRow>
                );
              })}
            </TableBody>
          </Table>
        </TableContainer>
        <Box p={2}>
          <TablePagination
            component="div"
            count={filteredAppsHealth.length}
            onPageChange={handlePageChange}
            onRowsPerPageChange={handleLimitChange}
            page={page}
            rowsPerPage={limit}
            rowsPerPageOptions={[5, 10, 25, 30]}
          />
        </Box>
      </Card>

      <DeleteAppHealthDialog
        id={deleteAppHealthId}
        open={openDeleteAppHealthDialog}
        onClose={handleCloseDeleteDialog}
        onSuccess={handleSuccessDeleting}
      />
    </>
  );
};

AppsHealthTable.propTypes = {
  appsHealth: PropTypes.array.isRequired
};

export default AppsHealthTable;
