import { Grid } from '@mui/material';

import AppsHealth from './Table';


const AppHealtPage = () => {
  return (
    <>
      <Grid
        container
        direction="row"
        justifyContent="center"
        alignItems="stretch"
        spacing={3}
      >
        <Grid item xs={12}>
          <AppsHealth />
        </Grid>
      </Grid>
    </>
  );
}

export default AppHealtPage;
