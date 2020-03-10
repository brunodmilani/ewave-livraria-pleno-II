import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-autores-create',
  templateUrl: './autores-create.component.html'
})
export class AutoresCreateComponent {
  public forecasts: WeatherForecast[];

  constructor(http: HttpClient,) {
  }
}
