$(document).ready(function () {
    loadDataOnReady();
    loadDataOnClick();
    changeSteps();
    nextStep();
    finishWizard(function () {
        //ON FINISH EVENT
    });
});


function finishWizard(onFinish) {
    var wizardContent = $('.wizard-content');
    var wizardButtons = $('.wizard-menu .list-group-item');
    var currForm = $('.wizard-content form');

    //Use document.body for dynamically loaded content listening
    $(document.body).on('click', '.wizard-fin', function (event) {
        var finButton = $(this);
        event.preventDefault();
        var currStep = parseInt($(this).attr('data-current-step'));
        //Get previous elements
        var nextStep = $('.step-' + (currStep + 1));
        var nextMenu = $('.step-' + (currStep + 1) + '-menu');
        var thisMenu = $('.step-' + currStep + '-menu');
        var thisStep = $('.step-' + currStep);

        //Update menu
        wizardButtons.removeClass('active');
        nextMenu.addClass('active');

        //Update button
        finButton.html('<img src="http://upload.wikimedia.org/wikipedia/commons/4/42/Loading.gif"> Please wait...');
        finButton.addClass('disabled');

        //AJAX SUBMIT FORM
        var getMethod = currForm.attr('data-method');
        var getAction = currForm.attr('data-action');

        $.ajax({
            url: getAction,
            type: getMethod,
            data: currForm.serialize(),
            success: function () {
                //AJAX success
                wizardContent.prepend('<div class="alert alert-success"><strong>That was successful!</strong> Please wait for the next step.</div>');

                thisMenu.addClass('success');

                //Update button
                finButton.html('Finish');
                finButton.removeClass('disabled');

                window.setTimeout(function () {
                    //UI
                    $('.alert').hide();

                    //FINISHED WIZARD
                    //FUNCTION HERE
                    onFinish();

                }, 500);
            },
            error: function () {
                //Ajax failure
                wizardContent.prepend('<div class="alert alert-danger"><strong>Something went wrong!</strong> Please try again.</div>');
                thisMenu.addClass('error');
                window.setTimeout(function () {
                    //Get "data-for" attribute and find element
                    var dataLoad = thisStep.attr('data-load');

                    //UI
                    $('.alert').hide();
                    thisMenu.removeClass('error');
                    thisMenu.addClass('active');

                    //Update button
                    finButton.html('Finish');
                    finButton.removeClass('disabled');

                    //        Check if attribute does exist
                    //        If true then load ajax, else load from div
                    if (typeof dataLoad !== 'undefined' && dataLoad !== false) {
                        //Load content ajax
                        wizardContent.load(dataLoad);
                    }
                    else {
                        wizardContent.html(currStep.html());
                    }
                }, 2000);
            }
        });

    });
}


function nextStep() {
    var wizardContent = $('.wizard-content');
    var wizardButtons = $('.wizard-menu .list-group-item');
    var currForm = $('.wizard-content form');

    //Use document.body for dynamically loaded content listening
    $(document.body).on('click', '.wizard-next', function (event) {
        var prevButton = $(this);
        event.preventDefault();
        var currStep = parseInt($(this).attr('data-current-step'));
        //Get previous elements
        var nextStep = $('.step-' + (currStep + 1));
        var nextMenu = $('.step-' + (currStep + 1) + '-menu');
        var thisMenu = $('.step-' + currStep + '-menu');
        var thisStep = $('.step-' + currStep);
        //Update menu
        wizardButtons.removeClass('active');
        nextMenu.addClass('active');

        //Update button
        prevButton.html('<img src="http://upload.wikimedia.org/wikipedia/commons/4/42/Loading.gif"> Please wait...');
        prevButton.addClass('disabled');

        //AJAX SUBMIT FORM
        var getMethod = currForm.attr('data-method');
        var getAction = currForm.attr('data-action');

        $.ajax({
            url: getAction,
            type: getMethod,
            data: currForm.serialize(),
            success: function () {
                //AJAX success
                wizardContent.prepend('<div class="alert alert-success"><strong>That was successful!</strong> Please wait for the next step.</div>');

                thisMenu.addClass('success');

                //Update button
                prevButton.html('Next step');
                prevButton.removeClass('disabled');

                window.setTimeout(function () {
                    //Get "data-for" attribute and find element
                    var dataLoad = thisStep.attr('data-load');

                    //UI
                    $('.alert').hide();

                    //        Check if attribute does exist
                    //        If true then load ajax, else load from div
                    if (typeof dataLoad !== 'undefined' && dataLoad !== false) {
                        //Load content ajax
                        wizardContent.load(dataLoad);
                    }
                    else {
                        wizardContent.html(nextStep.html());
                    }
                }, 500);
            },
            error: function () {
                //Ajax failure
                wizardContent.prepend('<div class="alert alert-danger"><strong>Something went wrong!</strong> Please try again.</div>');
                thisMenu.addClass('error');
                window.setTimeout(function () {
                    //Get "data-for" attribute and find element
                    var dataLoad = thisStep.attr('data-load');

                    //UI
                    $('.alert').hide();
                    thisMenu.removeClass('error');
                    thisMenu.addClass('active');

                    //Update button
                    prevButton.html('Next step');
                    prevButton.removeClass('disabled');

                    //        Check if attribute does exist
                    //        If true then load ajax, else load from div
                    if (typeof dataLoad !== 'undefined' && dataLoad !== false) {
                        //Load content ajax
                        wizardContent.load(dataLoad);
                    }
                    else {
                        wizardContent.html(currStep.html());
                    }
                }, 2000);
            }
        });

    });
}


function changeSteps() {
    var wizardContent = $('.wizard-content');
    var wizardButtons = $('.wizard-menu .list-group-item');

    //Use document.body for dynamically loaded content listening
    $(document.body).on('click', '.wizard-prev', function (event) {
        event.preventDefault();
        var currStep = parseInt($(this).attr('data-current-step'));

        //Get previous elements
        var prevStep = $('.step-' + (currStep - 1));
        var prevMenu = $('.step-' + (currStep - 1) + '-menu');

        //Update menu
        wizardButtons.removeClass('active');
        prevMenu.addClass('active');
        prevMenu.removeClass('success');

        //Get "data-for" attribute and find element

        var dataLoad = prevStep.attr('data-load');

        //        Check if attribute does exist
        //        If true then load ajax, else load from div
        if (typeof dataLoad !== 'undefined' && dataLoad !== false) {
            //Load content ajax
            wizardContent.load(dataLoad);
        }
        else {
            wizardContent.html(prevStep.html());
        }
    });
}

function loadDataOnReady() {
    var wizardContent = $('.wizard-content');
    //Get "data-for" attribute and find element
    var dataFor = $('.wizard-menu .list-group-item.active').attr('data-for');
    var elementFor = $(dataFor);

    var dataLoad = elementFor.attr('data-load');

    //        Check if attribute does exist
    //        If true then load ajax, else load from div
    if (typeof dataLoad !== 'undefined' && dataLoad !== false) {
        //Load content ajax
        wizardContent.load(dataLoad);
    }
    else {
        wizardContent.html(elementFor.html());
    }
}

function loadDataOnClick() {
    var wizardButtons = $('.wizard-menu .list-group-item');
    var wizardContent = $('.wizard-content');

    wizardButtons.on('click', function (event) {
        event.preventDefault();
        //Change active state
        wizardButtons.removeClass('active');
        $(this).addClass('active');


        //Get "data-for" attribute and find element
        var dataFor = $(this).attr('data-for');
        var elementFor = $(dataFor);

        var dataLoad = elementFor.attr('data-load');

        //        Check if attribute does exist
        //        If true then load ajax, else load from div
        if (typeof dataLoad !== 'undefined' && dataLoad !== false) {
            //Load content ajax
            wizardContent.load(dataLoad);
        }
        else {
            wizardContent.html(elementFor.html());
        }
    });
}