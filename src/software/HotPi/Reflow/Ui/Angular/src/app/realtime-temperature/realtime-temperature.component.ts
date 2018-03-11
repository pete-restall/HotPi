import { Component } from '@angular/core';

@Component({
  selector: 'hotpi-realtime-temperature',
  templateUrl: './realtime-temperature.component.html',
  styleUrls: ['./realtime-temperature.component.css']
})
export class RealtimeTemperatureComponent {
  private profileColour: string = '#ff00ff';

  public get chartOptions() {
    return {
      title: {
        text: 'Oven Temperature'
      },
      tooltip: {
        trigger: 'axis'
      },
      legend: {
        data: [
          {
            name: 'PROFILE'
          },
          {
            name: 'CURRENT'
          }
        ]
      },
      toolbox: {
        feature: {
          saveAsImage: {}
        }
      },
      grid: {
        left: '3%',
        right: '4%',
        bottom: '3%',
        containLabel: true
      },
      xAxis: [
        {
          type: 'time',
          axisLabel: {
            formatter: function(value) {
              value = new Date(value);
              let label: string = value.getMinutes() < 10 ? '0' : '';
              if (value.getSeconds() < 10){ 
                label += value.getMinutes() + ":0" + value.getSeconds();
              }
              else {
                label += value.getMinutes() + ":" + value.getSeconds();
              }

              return label;
            }
          }
        }
      ],
      yAxis: [
        {
          type: 'value',
		  min: 0,
		  max: 300
        }
      ],
      series: [
        {
          name: 'PROFILE',
          type: 'line',
          areaStyle: {normal: {}},
          data: this.createProfile()
        },
        {
          name: 'CURRENT',
          type: 'line',
          areaStyle: {normal: {}},
          data: this.createCurrent()
        }
      ]
    };
  }

  // TODO: STUBS TO GENERATE SOME DUMMY DATA.  WITH REAL DATA, USE BLUE FOR BELOW PROFILE, RED FOR ABOVE PROFILE.
  private createProfile(): any {
    let series = new Array();
    let datumTime: Date = new Date(0, 0, 0, 0, 0, 0);
    let datumTemperature: number = 10;
    for (let i = 0; i < 10; i++) {
      series.push({
        value: [datumTime, datumTemperature],
        itemStyle: { color: this.profileColour }
      });
      datumTime = new Date(datumTime.getTime() + 60000);
      datumTemperature += 10;
    }

    return series;
  }

  private createCurrent(): any {
    let series = new Array();
    let datumTime: Date = new Date(0, 0, 0, 0, 0, 0);
    let datumTemperature: number = 20;
    for (let i = 0; i < 10; i++) {
      series.push({
        value: [datumTime, datumTemperature],
        itemStyle: { color: i % 2 == 0 ? '#ff0000' : '#0000ff' }
      });
      datumTime = new Date(datumTime.getTime() + 60000);
      datumTemperature += 5;
    }

    return series;
  }
}
