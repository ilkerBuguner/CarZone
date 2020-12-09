import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'shortenUsername'
})
export class ShortenUsernamePipe implements PipeTransform {

  transform(value: string): string {
    return value?.length > 12 ? `${value.substr(0, 10)}...` : value;
  }
}
