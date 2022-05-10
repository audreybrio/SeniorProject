describe('GPA', function () {

    // User enters in all inforomation 
    it('Calculate GPA', function () {
        // Logs in 
        cy.visit('https://localhost:5002')


        cy.contains('Test').click()

        cy.contains('GPA/Grade Calculator').click()

        cy.contains('Calculate GPA').click()

        // Making selectiovns

        cy.get('#gradeOne').select(4)
        cy.get('#gradeTwo').select(2)
        cy.get('#gradeThree').select(7)
        cy.get('#gradeFour').select(6)

        cy.get('input[id="unit1"]').type('3')
        cy.get('input[id="unit2"]').type('3')
        cy.get('input[id="unit3"]').type('3')
        cy.get('input[id="unit4"]').type('3')

        // Saves
        cy.contains('Calculate GPA!').click()

        //cy.get('#gpaId').should('eq',3.075)

        //cy.contains("GPA is: 3.075")

        

    })

    // Missing grade selection choices 
    it('Missing Grade', function () {
        cy.visit('https://localhost:5002/gpaCalc')

        // Selections
        cy.get('input[id="unit1"]').type('3')
        cy.get('input[id="unit2"]').type('3')
        cy.get('input[id="unit3"]').type('3')
        cy.get('input[id="unit4"]').type('3')

        // Doesnt save, error message shown
        cy.contains('Calculate GPA!').click()

        cy.contains("Error: Missing Information")
    })

    // Missing Units 
    it('Missing Units', function () {
        cy.visit('https://localhost:5002/gpaCalc')

        // Selections
        cy.get('#gradeOne').select(4)
        cy.get('#gradeTwo').select(2)
        cy.get('#gradeThree').select(7)
        cy.get('#gradeFour').select(6)

        // Doesnt save, error message shown
        cy.contains('Calculate GPA!').click()

        cy.contains("Error: Missing Information")


    })


})
