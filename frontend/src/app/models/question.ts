import { Answer } from './answer';

export enum QuestionType {
  Teorija,
  Znakovi,
  Raskrsnice,
}

export interface Question {
  id?: number;
  text?: string;
  image?: string;
  points: number;
  order: number;
  questionType: QuestionType;
  answers?: Answer[];
}
