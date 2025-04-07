import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { WebscraperComponent } from "./Forms/webscraper/webscraper.component";


@Component({
  selector: 'app-root',
  imports: [WebscraperComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'Scraper.Client';

}

