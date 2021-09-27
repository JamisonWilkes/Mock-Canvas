


document.addEventListener('DOMContentLoaded', function () {
    var calendarEl = document.getElementById('calendar');

    var allEvents = [];
    var classMeetingTable = document.getElementById('classMeetingTable');
    var assignmentsDueTable = document.getElementById('assignmentsDueTable');

    var trElemsClass = classMeetingTable.getElementsByTagName('tr');
    var trElemsAssignments = assignmentsDueTable.getElementsByTagName('tr');

    //To create a json object for the assignments 
    for (var tr of trElemsAssignments) {
        var tdElems = tr.getElementsByTagName('td');
        var startDate = tdElems[3].innerText.split(" ", 1);
        var dueTime = tdElems[3].innerText.split(" ");

        startDate = new Date(startDate).toLocaleDateString('fr-CA');

        var eventObj = {
            id: tdElems[0].innerText,
            title: tdElems[1].innerText,
            start: startDate,
        };
        allEvents.push(eventObj);
    }

    //To create a json object for the class meeting times
    for (var tr of trElemsClass) {
        var tdElems = tr.getElementsByTagName('td');
        var dayOne;
        var dayTwo;
        var dayThree;


        if (tdElems[0].innerText == "-1") {

            var eventObj = {
                id: tdElems[1].innerText,
                title: tdElems[2].innerText,
                start: tdElems[3].innerText,

                end: tdElems[3].innerText,


                url: tdElems[2].innerHTML,
            };
            allEvents.push(eventObj);
        }
        else {
            if (tdElems[4] != ' ') {
                if (tdElems[4].innerText === "Sunday") { dayOne = 0 }
                else if (tdElems[4].innerText === "Monday") { dayOne = 1 }
                else if (tdElems[4].innerText === "Tuesday") { dayOne = 2 }
                else if (tdElems[4].innerText === "Wednesday") { dayOne = 3 }
                else if (tdElems[4].innerText === "Thursday") { dayOne = 4 }
                else if (tdElems[4].innerText === "Friday") { dayOne = 5 }
                else if (tdElems[4].innerText === "Saturday") { dayOne = 6 }
            }
            if (tdElems[5] != ' ') {
                if (tdElems[5].innerText === 'Sunday') { dayTwo = 0 }
                else if (tdElems[5].innerText === 'Monday') { dayTwo = 1 }
                else if (tdElems[5].innerText === 'Tuesday') { dayTwo = 2 }
                else if (tdElems[5].innerText === 'Wednesday') { dayTwo = 3 }
                else if (tdElems[5].innerText === 'Thursday') { dayTwo = 4 }
                else if (tdElems[5].innerText === 'Friday') { dayTwo = 5 }
                else if (tdElems[5].innerText === 'Saturday') { dayTwo = 6 }
            }
            if (tdElems[6] != ' ') {
                if (tdElems[6].innerText === 'Sunday') { dayThree = 0 }
                else if (tdElems[6].innerText === 'Monday') { dayThree = 1 }
                else if (tdElems[6].innerText === 'Tuesday') { dayThree = 2 }
                else if (tdElems[6].innerText === 'Wednesday') { dayThree = 3 }
                else if (tdElems[6].innerText === 'Thursday') { dayThree = 6 }
                else if (tdElems[6].innerText === 'Friday') { dayThree = 5 }
                else if (tdElems[6].innerText === 'Saturday') { dayThree = 6 }
            }
            var startTimeString = tdElems[2].innerText.split(" ", 2);
            var endTimeString = tdElems[3].innerText.split(" ", 2);

            var eventObj = {
                id: tdElems[0].innerText,
                title: tdElems[1].innerText,
                start: '2021-06-01',
                end: '2021-09-01',
                startTime: startTimeString[1],
                endTime: endTimeString[1],
                daysOfWeek: [dayOne, dayTwo, dayThree],

            };
            allEvents.push(eventObj);
        }


    }



    var calendar = new FullCalendar.Calendar(calendarEl, {
        initialView: 'dayGridMonth',
        eventColor: 'blue',
        events: allEvents,
        selectable: true,
        eventClick: function (info) {
            alert('Event: ' + info.event.title);
        }

    });
    calendar.render();
});