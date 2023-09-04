import { User } from './user';

export interface Instructor extends User {
  drivingLicense: string;
}
