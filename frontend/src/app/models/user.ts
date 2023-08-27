import { City } from './city';

export interface User {
  id?: number;
  firstName: string;
  lastName: string;
  email: string;
  username: string;
  city: City;
}
