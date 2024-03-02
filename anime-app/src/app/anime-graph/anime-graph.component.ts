import { Component, OnInit, effect, input } from '@angular/core';
import { HighchartsChartModule } from 'highcharts-angular';
import * as Highcharts from 'highcharts';
import { Anime } from '@app/@core/models';
import { JsonPipe } from '@angular/common';

@Component({
  selector: 'app-anime-graph',
  standalone: true,
  imports: [HighchartsChartModule, JsonPipe],
  templateUrl: './anime-graph.component.html',
  styleUrl: './anime-graph.component.scss',
})
export class AnimeGraphComponent implements OnInit {
  animes = input<Anime[]>([]);

  Highcharts: typeof Highcharts = Highcharts; // required
  chartOptions!: Highcharts.Options;

  constructor() {
    effect(() => {
      const categories = this.animes().map((anime) => anime.title);
      const seriesData = this.animes().map((anime) => ({
        name: anime.title,
        y: anime.votes.length,
        color: this.getRandomColor(),
      }));

      this.updateChart(categories, seriesData);
    });
  }

  ngOnInit() {
    this.applyBlackTheme();
  }

  private applyBlackTheme() {
    this.chartOptions = {
      chart: {
        type: 'column',
        backgroundColor: '#3c3c3d',
        style: {
          fontFamily: 'Arial',
        },
      },
      title: {
        text: 'Anime Votes',
        style: {
          color: '#FFFFFF',
        },
      },
      xAxis: {
        categories: [],
        labels: {
          style: {
            color: '#FFFFFF',
          },
        },
        lineColor: '#707073',
        tickColor: '#707073',
      },
      yAxis: {
        gridLineColor: '#707073',
        labels: {
          style: {
            color: '#FFFFFF',
          },
        },
        title: {
          text: 'Votes',
          style: {
            color: '#FFFFFF',
          },
        },
      },
      legend: {
        itemStyle: {
          color: '#E0E0E3',
        },
        itemHoverStyle: {
          color: '#FFF',
        },
        itemHiddenStyle: {
          color: '#606063',
        },
      },
      plotOptions: {
        column: {
          dataLabels: {
            enabled: true,
            style: {
              color: '#FFFFFF', // White data labels
            },
          },
        },
      },
      series: [
        {
          name: 'Votes',
          type: 'column',
          data: [],
        },
      ],
    };
  }

  private updateChart(categories: string[], seriesData: any[]) {
    // Update the chart if it's already rendered
    if (this.Highcharts.charts.length) {
      // Assuming it's the first chart
      const chart = this.Highcharts.charts[0];

      if (chart) {
        chart.update(
          {
            xAxis: {
              categories: categories,
            },
            series: [
              {
                name: 'Votes',
                type: 'column',
                data: seriesData,
              },
            ],
          },
          true // The 'true' flag redraws the chart
        );
      }
    }
  }

  private getRandomColor() {
    const letters = '0123456789ABCDEF';
    let color = '#';
    for (let i = 0; i < 6; i++) {
      color += letters[Math.floor(Math.random() * 16)];
    }
    return color;
  }
}
