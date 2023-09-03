import { ValidatorFn, AbstractControl, ValidationErrors } from '@angular/forms';

export function createDateFromFormat(dateString: string): Date | null {
  const dateComponents = dateString.split('/');

  if (dateComponents.length !== 3) {
    return null; // Invalid format
  }

  const isoDateString = `${dateComponents[2]}-${dateComponents[1]}-${dateComponents[0]}`;

  const dateObject = new Date(isoDateString);

  return isNaN(dateObject.getTime()) ? null : dateObject;
}

export function convertDate(dateString: string) {
  const date = new Date(dateString);
  const day = date.getDate() < 10 ? `0${date.getDate()}` : date.getDate();
  const month =
    date.getMonth() + 1 < 10 ? `0${date.getMonth() + 1}` : date.getMonth() + 1;
  const year = date.getFullYear();
  return `${day}/${month}/${year}`;
}

export function validateDateOfBirth(date: string): boolean {
  const dateParts = date.split('/');
  if (dateParts.length !== 3) {
    // Invalid date format (not in "dd/mm/yyyy" format)
    return false;
  }

  const day = parseInt(dateParts[0], 10);
  const month = parseInt(dateParts[1], 10);
  const year = parseInt(dateParts[2], 10);

  const currentYear = new Date().getFullYear();

  if (
    isNaN(day) ||
    isNaN(month) ||
    isNaN(year) ||
    day < 1 ||
    day > 31 ||
    month < 1 ||
    month > 12 ||
    year < 1900 ||
    year > currentYear
  ) {
    return false;
  }

  // Check for months with 30 days
  if ((month === 4 || month === 6 || month === 9 || month === 11) && day > 30) {
    return false;
  }

  // Check for February (28 or 29 days depending on leap year)
  if (month === 2) {
    if (day > 29) {
      return false;
    }

    // Check for leap year
    const isLeapYear = (year % 4 === 0 && year % 100 !== 0) || year % 400 === 0;

    if ((day === 29 && !isLeapYear) || day > 29) {
      return false;
    }
  }

  // Valid date
  return true;
}

export function dateOfBirthValidator(): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    if (!control.value) {
      return null;
    }

    if (!validateDateOfBirth(control.value)) {
      return { invalidDate: { value: control.value } };
    }
    return null;
  };
}
