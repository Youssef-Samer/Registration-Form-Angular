import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';

import { CitizensDetailsComponent } from './citizens-details/citizens-details.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, CitizensDetailsComponent],
  templateUrl: './app.component.html',
  styles: [],
})
export class AppComponent {
  title = 'CitizensApp';
}
