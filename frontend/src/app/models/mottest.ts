import { Vehicle } from './vehicle';

export interface MotTest {
  id?: number;
  description: string;
  date: string;
  mileage: number;
  vehicleId: number;
  vehicle?: Vehicle;
}
