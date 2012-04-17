window.addEvent('domready', function(){
  var transition = 'fade';
  $$('input[name=transition]').addEvent('click', function(){ transition = this.value; });
  var slideAvailable = ['slide-left', 'slide-right', 'slide-top', 'slide-bottom', 'fade'];
  var slideTransition = function(){
    switch(transition){
      case 'alternate':
        if(! $defined(this.count)) this.count = -1;
        return slideAvailable[++this.count % slideAvailable.length];
      case 'random': return slideAvailable.getRandom();
      default: return transition;
    }
  }
  
  $('option-auto').addEvent('click', function(){
    slideshow.options.auto = this.checked;
  });
  
  var slideshow = new BarackSlideshow('menu', 'pictures', 'loading', { transition: slideTransition, auto: true });

});