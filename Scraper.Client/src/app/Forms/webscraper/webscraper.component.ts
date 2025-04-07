import { Component, inject } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ScraperService } from '../../scraper.service';

import { DatePipe, NgFor, NgIf } from '@angular/common';
import { ScrapedResults } from '../../models/apiresponse';

@Component({
  selector: 'app-webscraper',
  imports: [ReactiveFormsModule, NgIf, NgFor, DatePipe],
  templateUrl: './webscraper.component.html',
  styleUrl: './webscraper.component.css'
})
export class WebscraperComponent {

  private scraperService = inject(ScraperService)

  scrapedResults: ScrapedResults[] = [];
  apiResponse : ScrapedResults[] = [];;
  positions: number[] = [];
  searchKeyword: string = "";
  showTable: boolean = false;

  ScraperForm : FormGroup = new FormGroup({
    SearchKeyword: new FormControl("", [
      Validators.required ]),
      SearchUrl : new FormControl("", [
      Validators.required ] ),
    SearchEngine : new FormControl("google", [
      Validators.required ]),
    MaxCount : new FormControl("100")  
  })

    onSubmit(): void {
      if (this.ScraperForm.invalid) {
        return; 
      }
  
      const formValues = this.ScraperForm.value;
      this.scraperService.insertSearchResults(formValues).subscribe({
        next: () => {
          this.fetchSearchResults();
          this.getPosition();
        },
        error: (err) => {
          console.error('Error inserting search results', err);
        }
      });

      this.ScraperForm.reset();
    }

    fetchSearchResults() {
      this.scraperService.fetchSearchResults().subscribe({
        next: (response: any) => {
          this.apiResponse = response; 
          this.scrapedResults = this.apiResponse.map((result, index) => ({
            id: result.id ?? index + 1,
            searchKeyword: result.searchKeyword,
            searchUrl: result.searchUrl,
            position: result.position,
            date: result.date ? new Date(result.date) : new Date()
          }));
          this.showTable = true;
        },
        error: (err) => {
          console.error('Error fetching data', err);
        }
      });
    }

    getPosition(){
      this.scraperService.getPositions().subscribe(
        (data) => {
          this.positions = data.position;
          this.searchKeyword = data.searchKeyword;
        },
        (error) => {
          console.error('Error fetching positions', error);
        });      
    }
}
