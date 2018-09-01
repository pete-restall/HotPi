# HotPi - A Pi Zero W Reflow Oven Controller

[![The Oven](https://github.com/pete-restall/HotPi/blob/master/doc/images/oven-front-800x600.jpg)](https://github.com/pete-restall/HotPi/blob/master/doc/images/oven-front.jpg)

## What is it ?
Years ago I bought a small oven for reflow conversion but never really got
around to it.  More recently I've had a need to reflow boards more accurately
than I can do 'by eye' so I decided to make some time and throw something
together using mainly bits I have to hand.

A &pound;9 Pi Zero W, an old project box that's probably older than I am, some
jellybean parts lying in my junk box, some nice 480V 25A SSRs I bought at the
same time as the oven, coupled with a few other cheap bits and pieces like an
open frame Mean Well IRM-10-5 5V 2A flyback converter should suffice.  Nothing
fancy, no bells and whistles.

The PCB was intended to be milled as it should have been a quicker turnaround
and cheaper since I have the copper clad and engraving bits already, although it
is much more labour intensive than outsourcing to China.  Unfortunately the
resolution required to mill the features of the MAX31850 was insufficient.

[![The Board](https://github.com/pete-restall/HotPi/blob/master/doc/images/board-in-box-400x300.jpg)](https://github.com/pete-restall/HotPi/blob/master/doc/images/board-in-box.jpg)

This is a personal one-off project and not intended for anybody else's
consumption.  But in case somebody's tempted then the usual disclaimers about
playing with mains voltages apply...

## Software
I had started off writing the software myself but it was turning out to be
a larger job than I wanted.  Then I stumbled across
[picoReflow](https://github.com/apollo-ng/picoReflow).  I spent a few hours
hacking some Python and ended up with a fully functional reflow controller
and UI in a fraction of the time it would take to roll my own.  Thanks !

The MAX31850 communication is a hand-cranked user-space bit-banging library
based around [BCM2835](http://www.airspayce.com/mikem/bcm2835/), basically
because there is no kernel support for the MAX31850 via the w1 module (and
the fallback w1-gpio is not good enough).
