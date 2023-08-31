import { City } from './city';

export enum Role {
  Admin,
  Customer,
  Employee,
  Lecturer,
  Instructor,
}

export interface User {
  id?: number;
  firstName: string;
  lastName: string;
  email: string;
  emailVerified: boolean;
  phoneNumber: string;
  dateOfBirth: string;
  role: Role;
  username: string;
  city: City;
  profileImage: string;
  isActive: boolean;
}
