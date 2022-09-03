import { Card } from '@mui/material';
import { useEffect, useState } from 'react';

import AppsHealthTable from './AppsHealthTable';

import APICallWrapper from 'src/api/APIWrapper/APICallWrapper';
import apiUrls from 'src/api/apiUrls';
import { AppHealth } from 'src/models/app_health';


const AppsHealth = () => {

  const updateAppHealthList = () => {

    //   APICallWrapper(
    //     {
    //       url: apiUrls.usermanagement.getUsers,
    //       options: {
    //         method: 'GET'
    //       },
    //       onSuccess: async (response) => {
    //         let allUsers = await response.json();

    //         setUsers(allUsers);
    //       }
    //     }
    //   )
  }

  const setNewAppHealthList = (appsHealth: AppHealth[]) => {
    setAppsHealth(appsHealth)
  }

  const [appsHealth, setAppsHealth] = useState<AppHealth[]>([]);

  useEffect(() => {
    updateAppHealthList();
  }, [])

  return (
    <Card>
      <AppsHealthTable appsHealth={appsHealth} setNewAppHealthList={setNewAppHealthList} />
    </Card>
  );
}

export default AppsHealth;
