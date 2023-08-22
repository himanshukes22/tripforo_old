var RHandler;


$(document).ready(function () {
    $(document).on("evoCalendar", "#calendar", function () {
        alert("");
    });
    RHandler = new ResHelper;
    RHandler.BindEvents();
   
});

var ResHelper = function () {
    this.FixDep_Sector = $("#ctl00_ContentPlaceHolder1_FixDep_Sector");
    this.FixDep_CCr = $("#calendar");
}
ResHelper.prototype.BindEvents = function () {
    var h = this;
    h.FixDep_CCr.hide();
   // h.myFunc();
    h.FixDep_Sector.change(function () {
        h.FixDep_CCr.show();        
        //h.FixDep_CCr.evoCalendar('addCalendarEvent', [
        //    {
        //        id: 'kNybja6',
        //        name: 'Mom\'s Birthday',
        //        date: 'May 27, 1965',
        //        type: 'birthday',
        //        everyYear: true // optional
        //    },
        //    {
        //        id: 'asDf87L',
        //        name: 'Graduation Day!',
        //        date: 'March 21, 2020',
        //        type: 'event'
        //    }
        //]);
    });
   
  
  // h.myFuncnew()
 


}

var eventDataArray = new Array();

function convertToDate(DepDate) {

    var d = DepDate.substring(0, 2);
    var m = DepDate.substring(2, 4);
    var y = "20" + DepDate.substring(4, 6);
    var date = new Date(y, m - 1, d);
    return date;

}



