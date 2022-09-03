interface MonitorTime {
  startTime: string;
  endTime: string;
}

export interface AppHealth {
  id: string;
  name: string;
  url: string;
  method: string;
  status: string;
  lastUpdateDate: Date;
  monitorTimes: MonitorTime[];
}
