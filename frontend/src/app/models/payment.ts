import { Customer } from './customer';

export interface Payment {
  id?: number;
  amount: number;
  description: string;
  date: string;
  customerId: number;
  customer?: Customer;
}
