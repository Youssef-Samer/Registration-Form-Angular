import { bootstrapApplication } from '@angular/platform-browser';
import { provideAnimations } from '@angular/platform-browser/animations';  // Make sure to import provideAnimations
import { appConfig } from './app/app.config';
import { AppComponent } from './app/app.component';
import { provideToastr } from 'ngx-toastr';

bootstrapApplication(AppComponent, {
  providers: [
    provideAnimations(),  // Add this to the providers array
    provideToastr(),
    ...appConfig.providers // Spread the providers from appConfig into the array
  ]
})
  .catch((err) => console.error(err));
