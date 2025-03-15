// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

document.addEventListener('DOMContentLoaded', function () {
    var calendarEl = document.getElementById('calendar');
    var calendar = new FullCalendar.Calendar(calendarEl, {
        initialView: 'dayGridMonth', // Month view
        headerToolbar: {
            left: 'prev,next today',
            center: 'title',
            right: 'dayGridMonth,timeGridWeek,timeGridDay'
        },
        events: [
            { title: 'Meeting', start: '2024-03-15T10:00:00' },
            { title: 'Project Deadline', start: '2024-03-18' },
            { title: 'Lunch', start: '2024-03-20T12:00:00' }
        ],
        schedulerLicenseKey: 'GPL-My-Project-Is-Open-Source'
    });
    calendar.render();
});
