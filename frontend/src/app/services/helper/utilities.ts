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
