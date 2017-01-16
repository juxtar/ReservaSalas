import { Component, OnInit, Input } from '@angular/core';

import { Sala } from '../_models';

@Component({
  selector: 'app-sala-detail',
  templateUrl: './sala-detail.component.html',
  styleUrls: ['./sala-detail.component.scss']
})
export class SalaDetailComponent implements OnInit {
  @Input()
  sala: Sala;

  get detail() {
    if (this.sala.ID == 1) {
      return "Disponible"
    }
    else {
      return "Reservada de 9 a 11"
    }
  }

  constructor() { }

  ngOnInit() {
  }

}
