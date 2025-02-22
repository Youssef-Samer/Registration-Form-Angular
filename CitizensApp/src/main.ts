import { bootstrapApplication } from '@angular/platform-browser';
import { provideAnimations } from '@angular/platform-browser/animations';
import { appConfig } from './app/app.config';
import { AppComponent } from './app/app.component';
import { provideToastr } from 'ngx-toastr';

bootstrapApplication(AppComponent, {
  providers: [
    provideAnimations(),
    provideToastr(),
    ...appConfig.providers
  ]
})
  .catch((err) => console.error(err));
