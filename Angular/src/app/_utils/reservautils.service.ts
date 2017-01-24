import { Injectable } from '@angular/core';
import { ITdDataTableColumn } from '@covalent/core';

import { Reserva } from '../_models';

@Injectable()
export class ReservautilsService {

  constructor() { }

  getDayFromISO(ISOdate: string): string {
    return (new Date(ISOdate)).toLocaleDateString();
  }

  getTimeFromISO(ISOdate: string): string {
    let time = (new Date(ISOdate)).toLocaleTimeString();
    let formatted = time.split(':');
    formatted.splice(2);
    return formatted.join(':');
  }

  filterReservas(data: Reserva[], searchTerm: string,
    columns: ITdDataTableColumn[]): Reserva[] {
      let filterFormat = (value, key) => {
        let ret;
        if (key.format) {
          ret = key.format(value);
        }
        ret = value;
        if (typeof ret != "string")
          return false
        return ret.toLowerCase().includes(searchTerm.toLowerCase());
      }

      var filteredData: Reserva[] = [];
      for(let i=0; i<columns.length; i++) {
        let key = columns[i];
        if (key.name.includes('.')) {
          let keys = key.name.split('.');
          let filter = data.filter((value: Reserva) => {
            let ret: any = value;
            for(let j=0; j<keys.length; j++)
              ret = ret[keys[j]];
            return filterFormat(ret, key);
          })
          filteredData = filteredData.concat(filter);
        }
        else {
          let filter = data.filter((value: Reserva) => {
            return filterFormat(value[key.name], key);
          })
          filteredData = filteredData.concat(filter);
        }
      }
      return Array.from(new Set(filteredData));
    }
}
