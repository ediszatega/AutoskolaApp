import { Category } from './category';

export interface Vehicle {
  id?: number;
  model: string;
  make: string;
  registration: string;
  categoryId: number;
  category?: Category;
}
