var SHandlerE;
$(document).ready(function () {
    SHandlerE = new SearchHelperE;
    SHandlerE.BindEvents()
});
var SearchHelperE = function () {
    this.minusF = $('.minusF');
    this.plusF = $('.plusF');
}
SearchHelperE.prototype.BindEvents = function () {
    var M = this;
    M.minusF.click(function () {
        var $input = $(this).parent().find('input');
        var $inputid = $input.attr('id');
        var count = parseInt($input.val()) - 1;
        //if ($inputid != "Adult" ) {
        //    count = count <= 0 ? 0 : count;
        //}
        //else {
        //    count = count < 1 ? 1 : count;
        //}
        if ($inputid != "AdultF") {
            count = count <= 0 ? 0 : count;
        }
        else {
            count = count < 1 ? 1 : count;
        }
        $input.val(count);
        $input.change();
        AddAllPax();
        return false;
    });
    M.plusF.click(function () {
        var $input = $(this).parent().find('input');
        var $inputid = $input.attr('id');
        let inpcount = parseInt($input.val()) + 1;
        let paxA = parseInt($("#AdultF").val())
        let paxC = parseInt($("#ChildF").val());
        let paxI = parseInt($("#InfantF").val());
        
        if (inpcount >= 10) {
            return false;
        }
        if ((paxC + paxA) >= 9 && $inputid != "InfantF") {
            return false;
        }
        if (paxA > 9) {
            return false;
        }
        if (paxC > 9) {
            return false;
        }
        if (paxI >= paxA &&  $inputid != "ChildF") {
            return false;
        }
        $input.val(inpcount);
        $input.change();
        AddAllPax();
        return false;
    });

}

function AddAllPax() {
    let adultinp = $("#AdultF").val();
    let childinp = $("#ChildF").val();
    let infantinp = $("#InfantF").val();

    $("#sapnTotPaxF").val(parseInt(adultinp) + parseInt(childinp) + parseInt(infantinp) + " Traveler(s)");


}