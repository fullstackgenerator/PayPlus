// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

document.addEventListener('DOMContentLoaded', function() {
    var calendarEl = document.getElementById('calendar');
    var calendar = new FullCalendar.Calendar(calendarEl, {
        initialView: 'dayGridMonth',
        headerToolbar: {
            left: 'prev,next today',
            center: 'title',
            right: 'dayGridMonth,timeGridWeek,timeGridDay'
        },
        schedulerLicenseKey: 'GPL-My-Project-Is-Open-Source',
        events: '/Calendar/GetInvoiceEvents',
        eventClick: function(info) {
            // Show invoice details when clicked
            var services = info.event.extendedProps.services;
            var total = info.event.extendedProps.total;

            Swal.fire({
                title: info.event.title,
                html: `<p>Services: ${services}</p><p>Total: ${total}</p>`,
                confirmButtonText: 'OK'
            });
        }
    });
    calendar.render();
});

document.addEventListener('DOMContentLoaded', function () {
    // Calculate total price when services are selected
    $('select[name="ServiceIds"]').change(function () {
        var totalPrice = 0;
        $('select[name="ServiceIds"] option:selected').each(function () {
            var serviceText = $(this).text();
            var servicePrice = parseFloat(serviceText.split(' - ')[1].replace('€', '').trim());
            if (!isNaN(servicePrice)) {
                totalPrice += servicePrice;
            }
        });
        $('#TotalPrice').val(totalPrice.toFixed(2));
    });

    // Log form submission
    $('#create-offer-form').on('submit', function (e) {
        console.log('Form submitted');
        console.log('PartnerId:', $('#PartnerId').val());
        console.log('ServiceIds:', $('select[name="ServiceIds"]').val());
        console.log('TotalPrice:', $('#TotalPrice').val());
    });
});

// Client-side validation for date range
document.querySelector('form').addEventListener('submit', function (e) {
    const startDate = new Date(document.getElementById('StartDate').value);
    const endDate = new Date(document.getElementById('EndDate').value);

    if (startDate && endDate && endDate < startDate) {
        e.preventDefault();
        alert('End date must be after start date');
    }
});