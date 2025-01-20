<template>
    <div>
      <h2>Job Listings</h2>
      <div>
        <input
          type="text"
          v-model="filter"
          placeholder="Filter by title or company..."
          class="filter-input"
        />
      </div>
      <div v-if="loading">Loading jobs...</div>
      <div v-else-if="error">{{ error }}</div>
      <div v-else>
        <table class="job-table">
          <thead>
            <tr>
              <th>Title</th>
              <th>Company</th>
              <th>Salary Range</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="job in filteredJobs" :key="job.jobId">
              <td>{{ job.jobTitle }}</td>
              <td>{{ job.companyName }}</td>
              <td>
                {{ job.annualSalaryMin }} - {{ job.annualSalaryMax }}
                {{ job.salaryCurrency }}
              </td>
            </tr>
          </tbody>
        </table>
      </div>
    </div>
  </template>
  
  <script lang="ts">
  import { ref, computed, onMounted } from 'vue';
  import jobsService from '../services/jobsService';
  
  // Define the Job type
  interface Job {
    jobId: number;
    jobTitle: string;
    companyName: string;
    annualSalaryMin: number;
    annualSalaryMax: number;
    salaryCurrency: string;
  }
  
  export default {
    name: 'JobList',
    setup() {
      const jobs = ref<Job[]>([]); // Explicitly type jobs as an array of Job
      const loading = ref(true);
      const error = ref('');
      const filter = ref('');
  
      const fetchJobs = async () => {
        try {
          jobs.value = await jobsService.getAllJobs();
        } catch (err) {
          error.value = 'Failed to load jobs.';
        } finally {
          loading.value = false;
        }
      };
  
      const filteredJobs = computed(() => {
        const filterText = filter.value.toLowerCase();
        return jobs.value.filter(
          (job) =>
            job.jobTitle.toLowerCase().includes(filterText) ||
            job.companyName.toLowerCase().includes(filterText)
        );
      });
  
      onMounted(() => {
        fetchJobs();
      });
  
      return {
        jobs,
        loading,
        error,
        filter,
        filteredJobs,
      };
    },
  };
  </script>
  
  <style scoped>
  h2 {
    font-size: 1.5em;
    margin-bottom: 1em;
  }
  
  .filter-input {
    margin-bottom: 1em;
    padding: 0.5em;
    font-size: 1em;
    width: 100%;
  }
  
  .job-table {
    width: 100%;
    border-collapse: collapse;
    margin-top: 1em;
  }
  
  .job-table th,
  .job-table td {
    border: 1px solid #ccc;
    padding: 0.75em;
    text-align: left;
  }
  
  .job-table th {
    background-color: #f4f4f4;
  }
  
  .job-table tr:nth-child(even) {
    background-color: #f9f9f9;
  }
  </style>
  