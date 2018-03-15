import { Component, OnInit, OnDestroy } from '@angular/core';

import { Observable } from 'rxjs/Observable';

import { OvenTemperatureSampled } from '../oven-temperature-sampled-observable/oven-temperature-sampled';

const TEMPERATURE_BUFFER_SIZE: number = 10 * 60;
const PROFILE_COLOUR: string = '#ff00ff';
const JUST_RIGHT_COLOUR: string = '#00ff00';
const TOO_HOT_COLOUR: string = '#ff0000';
const TOO_COLD_COLOUR: string = '#0000ff';

@Component({
  selector: 'hotpi-realtime-temperature',
  templateUrl: './realtime-temperature.component.html',
  styleUrls: ['./realtime-temperature.component.css']
})
export class RealtimeTemperatureComponent implements OnInit, OnDestroy {
  private profileData: Array<any> = [];
  private temperatureData: Array<any> = [];
  private chart: any;
  private ovenTemperatureSampledSubscription: any;

  constructor(private ovenTemperatureSampled: Observable<OvenTemperatureSampled>) {
  }

  public ngOnInit() {
    let _this = this;
    this.ovenTemperatureSampledSubscription = this.ovenTemperatureSampled.subscribe(
      (observed: OvenTemperatureSampled) => _this.onOvenTemperatureSampled(observed));
  }

  public chartOnInit(chart: any) {
    if (typeof(chart) === 'undefined' || chart == null)
      throw new ReferenceError('Invalid (undefined or null) ECharts instance passed to the onInit event.');

    this.chart = chart;
  }

  private onOvenTemperatureSampled(observed: OvenTemperatureSampled) {
    console.log('TEMP IS ' + observed.temperature.celsius);
    if (this.temperatureData.length >= TEMPERATURE_BUFFER_SIZE) {
      this.temperatureData.shift();
    }

    this.temperatureData.push({
      value: [observed.timestamp, observed.temperature.celsius],
      itemStyle: { color: JUST_RIGHT_COLOUR }
    });

    if (typeof(this.chart) === 'undefined')
      return;

    this.chart.setOption({
      series: [
        {
          name: 'PROFILE',
          type: 'line',
          areaStyle: {normal: {}},
          data: this.profileData
        },
        {
          name: 'CURRENT',
          type: 'line',
          areaStyle: {normal: {}},
          data: this.temperatureData
        }
      ]
	});
  }

  public ngOnDestroy() {
    if (typeof(this.ovenTemperatureSampledSubscription) !== 'undefined' && this.ovenTemperatureSampledSubscription != null)
      this.ovenTemperatureSampledSubscription.unsubscribe();
  }

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
          data: []
        },
        {
          name: 'CURRENT',
          type: 'line',
          areaStyle: {normal: {}},
          data: []
        }
      ]
    };
  }
}
