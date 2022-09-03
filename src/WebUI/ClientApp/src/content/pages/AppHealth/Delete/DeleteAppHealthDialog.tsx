import PropTypes from 'prop-types';
import { Grid } from '@mui/material';
import DialogTitle from '@mui/material/DialogTitle';
import Dialog from '@mui/material/Dialog';

import ButtonSuccess from 'src/components/Buttons/ButtonSuccess';
import ButtonError from 'src/components/Buttons/ButtonError';
import APICallWrapper from 'src/api/APIWrapper/APICallWrapper';
import apiUrls from 'src/api/apiUrls';


const DeleteAppHealthDialog = (props) => {
  const { onClose, onSuccess, open, appHealth } = props;

  const handleClose = () => {
    onClose();
  };

  const handleDeleteUser = () => {

    APICallWrapper(
      {
        url: `${apiUrls.usermanagement.deleteUser}?Id=${userId}`,
        options: {
          method: 'DELETE'
        },
        onSuccess: async (response) => {
          onSuccess();
          onClose();
        },
        onFailure: async (response) => {
          onClose();
        },
        showSuccess: true,
        successMesage: "User deleted"
      }
    )
  };

  return (
    <Dialog onClose={handleClose} open={open} >
      <Grid sx={{ padding: 2 }}>
        <DialogTitle>Delete health check "{appHealth.name}"?</DialogTitle>
        <Grid container flexDirection={"row"} justifyContent={"space-evenly"}>
          <Grid item>
            <ButtonSuccess size="medium" text="Delete" onClick={handleDeleteUser} />
          </Grid>
          <Grid item>
            <ButtonError size="medium" text="Cancel" onClick={handleClose} />
          </Grid>
        </Grid>
      </Grid>
    </Dialog>
  );
}

DeleteAppHealthDialog.propTypes = {
  onClose: PropTypes.func.isRequired,
  onSuccess: PropTypes.func.isRequired,
  open: PropTypes.bool.isRequired,
  id: PropTypes.string.isRequired
};

export default DeleteAppHealthDialog;
