import { User } from './user';

export interface News {
  id?: number;
  title?: string;
  text?: string;
  image?: string;
  date?: string;
  user?: User;
}
