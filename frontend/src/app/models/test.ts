import { Category } from './category';
import { Question } from './question';

export interface Test {
  id?: number;
  description?: string;
  categoryId: number;
  category?: Category;
  questions?: Question[];
}
